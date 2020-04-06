using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DH_Core
{
    /// <summary>
    /// 2017.08.07 이보현 DB조회 내용 추가
    /// /// return Param 은 단일 컨트롤 사용 Param, 다중 컨트롤 사용 Param으로 나누어져있음
    /// Case 별로 나누어서 사용
    /// </summary>
    class GetDBInfo_Common
    {
        #region Attributes (속성정의 집합)
        static _Environment env;

        #endregion

        #region DB CRUD(데이터베이스 처리)
        static public DataSet DB_select(int Index, object[] Param, out string error_msg)
        {
            env = new _Environment(); //로그인 시 설정한 환경 변수 대입
            //일반 콤보(다중)
            //(부서) Param :
            //(거래처) Param :
            //일반 콤보(다중)
            //(창고|공정, 장소|작업장) Param : 조회 조건 정보 [0] : 1번째 콤보박스 명칭 [1] : 2번째 콤보박스 명칭 [2] : BASELOC_FG 0:창고 1:생산 [3] : 전체 : 0 전체미포함 : 1 [4] : OPER --> 해당 OPER는 하드 코딩을 대비한 파라미터임(고객사 요청 Ex): 불량 창고만 보여주세요.)

            //공통 콤보
            //(공통코드-TS_CODE,TB_CODE) Param : 조회 조건 정보 [0] : 콤보박스 명칭, [0] : C_ID [1] : 0-전체포함, 1-미포함 ///////// -- TS_CODE,TB_CODE 조회
            //외에 추가 작업은 위 칸에 주석 업데이트 필수
            return df_select(Index, Param, out error_msg); //SELECT할 DB INDEX, Param, 출력 메시지
        }

        #region DB Select index 설정 구간
        //////////////////////////////////////////
        // 일반 콤보 내용 추가
        // OPER  : DB_select 1, 사용//
        // LOC  : DB_select 2, 사용//
        // DEPT : DB_select 3, 사용//
        // CUST : DB_select 4, 사용//

        //JNKC 공통 내용 관리 추가
        //공통코드 시스템(TS_CODE) : 98, 사용 //
        //공통코드 기준정보(TB_CODE) : 99, 사용 //


        //외에 추가 작업은 위 칸에 주석 업데이트 필수
        //////////////////////////////////////////

        static public int DB_SINGLE_Param(String Type) //하나의 LookupEdit 컨트롤 사용 시
        {
            int rtn_DB_Param = 0;
            switch (Type)
            {
                case "공 정(배합)": //작업장 조회 시
                    {
                        rtn_DB_Param = 2;
                    }
                    break;
                case "TS공통": //(시스템)
                    {
                        rtn_DB_Param = 98;
                    }
                    break;
                case "TB공통": //(기준정보)
                    {
                        rtn_DB_Param = 99;
                    }
                    break;
            }
            return rtn_DB_Param; //SELECT할 DB INDEX, Param, 출력 메시지
        }
        static public int[] DB_NUMEROUS_Param(String Type) //복수의 LookupEdit 컨트롤 사용 시
        {
            //////////////////////////////////////////
            //////////////////////////////////////////

            int[] rtn_DB_Param = null;

            switch (Type)
            {
                case "공정/작업장":
                {
                    rtn_DB_Param = new int[] {1,2}; // 1: 창고(OPER) 조회 2: 창고(OPER)에 따른 장소(LOC) 조회
                }
                break;
            }
            return rtn_DB_Param; //SELECT할 DB INDEX, Param, 출력 메시지
        }

        static public String[] Set_Param(String Type) //기본 파라미터를 저장해 놓을 때 사용
        {
            String[] rtn_Set_Param = null;
            switch (Type)
            {
                case "공 정(배합)":
                    {
                        rtn_Set_Param = new String[] {"1","0","MX"}; // [0] : 공정 [1] : 전체 포함 [2] : 공정(고객사 프로세스 - YMT : 배합 : MX)
                    }
                    break;
            }
            return rtn_Set_Param; //SELECT할 DB INDEX, Param, 출력 메시지
        }
        #endregion
        /// <summary>
        /// ////////////////////////////////////////////
        /// ////////////2017.08.04 이보현///////////////
        /// ///////// DB 내용 추가 부분(ICUBE)//////////
        /// ////////////////////////////////////////////
        /// </summary>

        static private DataSet df_select(int Index, object[] Param, out string error_msg)
        {
            DataSet ds = null;
            gConst.DbConn.ClearDB();
            error_msg = "";
            switch (Index)
            {
                case 1: //공정
                    {
                        gConst.DbConn.ProcedureName = "USP_COM_GET_OPER_COMBO_";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        //gConst.DbConn.AddParameter(new SqlParameter("@PLANT", env.PLANT)); //IU 사용
                        gConst.DbConn.AddParameter(new SqlParameter("@BASELOC_FG", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@FIX_OPER", Param[2]));
                        ds = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 2: //작업장
                    {
                        gConst.DbConn.ProcedureName = "USP_COM_GET_LOC_COMBO_";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        //gConst.DbConn.AddParameter(new SqlParameter("@PLANT", env.PLANT)); //IU 사용
                        gConst.DbConn.AddParameter(new SqlParameter("@BASELOC_FG", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@FIX_OPER", Param[2]));
                        gConst.DbConn.AddParameter(new SqlParameter("@OPER", Param[3])); //고정 된 공정
                        ds = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 3: //부서
                    {
                        gConst.DbConn.ProcedureName = "USP_COM_GET_DEPT_COMBO_";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        //gConst.DbConn.AddParameter(new SqlParameter("@PLANT", env.PLANT)); //IU 사용
                        gConst.DbConn.AddParameter(new SqlParameter("@BASELOC_FG", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@FIX_OPER", Param[2]));
                        gConst.DbConn.AddParameter(new SqlParameter("@OPER", Param[3])); //고정 된 공정
                        ds = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 4: //거래처
                    {
                        gConst.DbConn.ProcedureName = "USP_COM_GET_CUST_COMBO_";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        //gConst.DbConn.AddParameter(new SqlParameter("@PLANT", env.PLANT)); //IU 사용
                        gConst.DbConn.AddParameter(new SqlParameter("@BASELOC_FG", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        gConst.DbConn.AddParameter(new SqlParameter("@FIX_OPER", Param[2]));
                        gConst.DbConn.AddParameter(new SqlParameter("@OPER", Param[3])); //고정 된 공정
                        ds = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                case 98: //TS_CODE
                    {
                        gConst.DbConn.ProcedureName = "USP_COM_GET_TS_CODE_COMBO";
                        gConst.DbConn.AddParameter(new SqlParameter("@C_ID", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        ds = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;

                case 99: //TB_CODE
                    {
                        gConst.DbConn.ProcedureName = "USP_COM_GET_TB_CODE_COMBO";
                        gConst.DbConn.AddParameter(new SqlParameter("@COMP", env.Company));
                        gConst.DbConn.AddParameter(new SqlParameter("@FACT", env.Factory));
                        //gConst.DbConn.AddParameter(new SqlParameter("@PLANT", env.PLANT)); //IU 사용
                        gConst.DbConn.AddParameter(new SqlParameter("@C_ID", Param[0]));
                        gConst.DbConn.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        ds = gConst.DbConn.GetDataSetQuery(out error_msg);
                    }
                    break;
                default: break;
            }
            gConst.DbConn.DBClose();
            return ds;
        }
        #endregion
    }
    /// <summary> 컨트롤 호출 모음
    /// jnkcLookUpEdit_Single LookUpEditItem_ACCT = ledt_ITEM_ACCT; //계정구분 호출 정의
    /// ledt_ITEM_ACCT = LookUpEditItem_ACCT.SetComboBox("TS공통", new String[] {"계 정 구 분"}, new String[] { "ITEM_ACCT", "0" }); //세팅
    /// 
    /// jnkcLookUpEdit_EditvalueChanged LookUpEditOPER_LOC = ledt_OPER_LOC; //공정/작업장 호출 정의 (단일 공정 조회,전체) 에 맞게 사용
    /// ledt_OPER_LOC = LookUpEditOPER_LOC.SetComboBox("공정/작업장",new String[] { "공 정(배합)", "작   업   장" }, new String[] { "1", "0", "MX", "%" }); //해당 공정만 조회(전체 조회 시 [2]를 "MX" -> "%" 로 변경) //세팅
    /// </summary>
}
