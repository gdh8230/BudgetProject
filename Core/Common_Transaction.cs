using System.Data;
using System.Data.SqlClient;

namespace EWMES_Core
{
    public class Common_Transaction
    {


        #region Attributes (속성정의 집합)
        ////////////////////////////////
        string[] gParam = null;
        //string gCODE = null;
        //string gNAME = null;
        //string gFACT = null;
        //string FACT = null;

        //필수요소
        //public string CODE { get { try { return gCODE; } catch { return ""; } } }
        //public string NAME { get { try { return gNAME; } catch { return ""; } } }
        //public string FACT { get { try { return gFACT; } catch { return ""; } } }

        #endregion

        #region DB CRUD(데이터베이스 처리)
        ///////////////////////////////
        /// <summary>
        /// DB Select
        /// </summary>
        /// <param name="Index"></param>
        /// <param name="Param"></param>
        /// <returns></returns>
        public static DataSet df_Transaction(int Index, object[] Param)
        {
            // Index 1 ~  9  : ComboBox관련 등록관련 쿼리 또는 프로시져
            // Index 10 ~ 19 : Select관련 쿼리 또는 프로시져
            // Index 20 ~ 29 : Transaction 쿼리 또는 프로시져
            // Param[0] : Company
            // Param[1] : Factory

            DataSet DT = null;
            _Environment env = new _Environment();
            string Errchk;          
            ////gConst.DbConn_ERP.ClearDB();
            gConst.DbConn_MES.ClearDB();
            switch (Index)
            {
                case 0: //현재화면 사용가능여부 조회
                    {
                        string Query = "SELECT      ACCE " +
                                      "FROM        TS_ACCESS WITH(NOLOCK) " +
                                      "WHERE       COMP ='" + Param[0] + "' " +
                                      "AND         FACT ='" + Param[1] + "' " +
                                      "AND         USR  ='" + Param[2] + "' " +
                                      "AND         WIND ='" + Param[3] + "' ";
                        DT = gConst.DbConn_MES.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
                case 1: //공통코드 조회
                    {
                        //공통코드에서 가져올때 사용
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_tb_code";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@C_ID", Param[2]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@STAT", Param[3]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[4])); //0=전체 포함 , 1=전체 제외
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;

                case 2: //품목의 계정구분 조회
                    {
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_ICUBE_ACCT_FG";
                        
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        //gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[2]));//0=전체 포함 , 1=전체 제외
                        
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;

                case 3: //창고/공정/외주 또는 장소/작업장/외주처 정보 조회(LOOK UP EDIT용)
                    {
                        //gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_VL_SLOC_INFO_MES";//USP_COM_GET_VL_SLOC_INFO";
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_TB_OPER";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[2]));         //0 = 전체포함 ,  1 = 전체 미포함
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@BASELOC_FG", Param[3]));   //0 = 창고,      1 = 생산(공정),    2 = 외주
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@WC_TYPE", Param[4]));      //0 = 공정,      1 = 작업장
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@LOC_CD", Param[5]));
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;
                case 4: //ERP 공통코드 조회
                    {
                        //--  AG	지역그룹구분
                        //--  AM	지역관리구분
                        //--  LA	재고조정구분
                        //--  LE	수입제비용 구분
                        //--  LP	구매자재구분
                        //--  LQ	품질검사구분
                        //--  LS	영업관리구분
                        //--  P1	생산 설비
                        //--  P2	작업팀
                        //--  P3	작업 SHIFT
                        //--  PC	결재 조건
                        //--  PU	구매단가유형
                        //--  SU	영업단가유형
                        //--  TM	거래처 분류
                        //--  TS	배송 방법
                        //--  V1	고객성향구분
                        //--  V2	고객등급구분
                        //--  WD	대여 구분
                        //--  WE	위탁적재장소
                        //--  Z1	품목 대분류
                        //--  Z2	품목 중분류
                        //--  Z3	품목 소분류

                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_ICUBE_LCTRL_MGM_D";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        //gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[2]));         //-- 0  = 전체포함    , 1  = 전체 미포함
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@CTRL_CD", Param[3]));      //-- P2 = 작업팀      , P3 = 작업조
                    }
                    break;
                case 5: //창고/공정/외주 또는 장소/작업장/외주처 정보 조회 (GRID 표시용)
                    {
                        //gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_VL_SLOC_INFO_D";
                        //gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_VL_SLOC_INFO_MES";
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_TB_OPER";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));             
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@BASELOC_FG", Param[2]));       //0 = 창고,      1 = 생산(공정),    2 = 외주
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@WC_TYPE", Param[3]));          //0 = 공정,      1 = 작업장
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@LOC_CD", Param[4]));           //0 = 전체포함 ,  1 = 전체 미포함
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", "1"));
                        
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;
                case 6: // 작업자 조회
                    {
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_TB_MAN";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[2]));
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;
                case 7: // 사업장 조회
                    {
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_TB_FACT";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        //gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;
                case 8: // 품목 그룹 조회
                    {
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_TB_ITEM_GROUP";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        //gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[1]));
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;
                case 9: // 불량군/불량유형 조회
                    {
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_TB_BAD";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@BAD_GBN", Param[2]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@BAD_GRUP", Param[3]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[4]));
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;
                case 10: //라인 정보를 조회
                    {
                        string Query = "SELECT RTRIM(LINE) CODE, RTRIM(LINE_NAME) NAME,  " +
                                       "RTRIM(FACT) FACT " +
                                       "FROM TB_LINE " +
                                       "WHERE FACT like '" + Param[0] + "%' " +
                                       "AND USEYN = 'Y' " +
                                       "ORDER BY CODE ";
                        DT = gConst.DbConn_MES.GetDataSetQuery(Query, out Errchk);
                    }
                    break;
                case 11: //설비정보를 조회
                    {
                        gConst.DbConn_MES.ProcedureName = "dbo.USP_COM_GET_TB_MACH_COMBO";
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@COMP", Param[0]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@FACT", Param[1]));
                        gConst.DbConn_MES.AddParameter(new SqlParameter("@TYPE", Param[2]));
                        DT = gConst.DbConn_MES.GetDataSetQuery(out Errchk);
                    }
                    break;

            }
            return DT;
        }
        #endregion 


    }
}

