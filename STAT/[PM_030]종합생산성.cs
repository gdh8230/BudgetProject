using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using TIMS;
using System.Data.SqlClient;
using System.IO;

namespace TIMS_PM
{
    public partial class PM_030 : DevExpress.XtraEditors.XtraForm
    {
        string Header = string.Empty;
        Common Common_Module = new Common();
        SqlDBHelper helper = new SqlDBHelper(false);
        DEVControl DevControl = new DEVControl();

        public PM_030()
        {
            InitializeComponent();
        }

        #region " 폼 로드 "
        private void PM_030_Load(object sender, EventArgs e)
        {
            DevControl.DO_SET_FOCUSED_ROWGRID_COLOR(SS1_View);

            SS1_View.IndicatorWidth = 50;
            SS1_View.CustomDrawRowIndicator += new DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventHandler(DevControl.Grid_RowNumver);

            Do_New();
        }
        #endregion

        #region " 메인 폼에 대한 이벤트 함수 "
        public void Do_Event_Menu(string Key)
        {
            switch (Key)
            {
                case "InqFunc":
                    ((TIMS_MAIN.Main_Form)this.ParentForm).Start_Progress();
                    Do_Search();
                    break;
                case "SaveFunc":
                    Do_Save();
                    break;
                case "ExcelFunc":
                    Do_Excel();
                    break;
                case "RowNewFunc":
                    Do_RowNew();
                    break;
                case "RowDelFunc":
                    Do_RowDel();
                    break;
                case "DelFunc":
                    Do_Delete();
                    break;
                case "NewFunc":
                    Do_New();
                    break;
            }

            ((TIMS_MAIN.Main_Form)this.ParentForm).Close_Progress();
        }
        #endregion

        #region " Do_Search "
        private void Do_Search()
        {
            try
            {
                DateTime WorkDate_From = dtp_WorkDate_From.DateTime;
                DateTime WorkDate_To = dtp_WorkDate_To.DateTime;

                string sql = string.Empty; 
                DataTable G_DT = new DataTable();
                DataTable S_DT = new DataTable();

                #region " 근무 일수 정보 조회 "
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		select '00' AS Seq,'근무일수' Header2, m.Wccode, cast(isnull(sum(s.workingday),0) as float) Work_Count" + Environment.NewLine;
                sql += "		from BI_Team_mapping as m" + Environment.NewLine;
                sql += "			left outer join  (" + Environment.NewLine;
			    sql += "		                    SELECT t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7) as condate, COUNT(*) as workingday" + Environment.NewLine;
			    sql += "		                    FROM (" + Environment.NewLine;
				sql += "		                        SELECT c.Code_Name as business, M.WcCode, M.condate" + Environment.NewLine;
				sql += "		                        FROM BI_Daily_Worker_Management AS M" + Environment.NewLine;
				sql += "			                        LEFT OUTER JOIN BI_WorkCalendar		AS A ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
				sql += "			                        left outer join BI_WorkCenter_Matching		as b on m.Wccode = b.wccode" + Environment.NewLine;
				sql += "			                        LEFT OUTER JOIN T_Info_MasterCode	AS c ON c.Major_Code = 'BUSINESS' AND b.Business = c.Minor_Code" + Environment.NewLine;
                sql += "		                        WHERE isnull(B.Business,'') <> '' and M.Wccode <> 'S01' AND (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1'" + Environment.NewLine;
				sql += "		                        GROUP BY c.Code_Name, M.Wccode, M.condate" + Environment.NewLine;
				sql += "		                        ) as T" + Environment.NewLine;
			    sql += "		                    GROUP BY t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7)" + Environment.NewLine;
                sql += "		                    ) as S on m.WcCode = s.WcCode" + Environment.NewLine;
                sql += "		group by m.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "     SELECT '00' AS Seq,'근무일수' Header2, " + Environment.NewLine;
                sql += "				  case when z.business = 'Gaming' and z.division<>'G' then 'BLU'  " + Environment.NewLine;
                sql += "						 else z.business end WcCode, " + Environment.NewLine;
                sql += "				  max(z.Sub_Total) Sub_Total " + Environment.NewLine;
                sql += "		from (" + Environment.NewLine;
                sql += "		    select s.business, s.Wccode, sum(s.workingday) Sub_Total,case when substring(s.wccode,1,1) = 'A' then substring(s.wccode,1,1) else substring(s.business,1,1) end division" + Environment.NewLine;
                sql += "		    from (" + Environment.NewLine;
                sql += "		        SELECT t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7) as condate, COUNT(*) as workingday" + Environment.NewLine;
                sql += "		        FROM (" + Environment.NewLine;
                sql += "			        SELECT c.Code_Name as business, M.WcCode, M.condate" + Environment.NewLine;
                sql += "			        FROM BI_Daily_Worker_Management AS M" + Environment.NewLine;
                sql += "				        LEFT OUTER JOIN BI_WorkCalendar		AS A ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "				        left outer join BI_WorkCenter_Matching		as b on m.Wccode = b.wccode" + Environment.NewLine;
                sql += "				        LEFT OUTER JOIN T_Info_MasterCode	AS c ON c.Major_Code = 'BUSINESS' AND b.Business = c.Minor_Code" + Environment.NewLine;
                sql += "			        WHERE isnull(B.Business,'') <> '' and M.Wccode <> 'S01' AND (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1'" + Environment.NewLine;
                sql += "			        GROUP BY c.Code_Name, M.Wccode, M.condate" + Environment.NewLine;
                sql += "			        ) as T" + Environment.NewLine;
                sql += "		        GROUP BY t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7)" + Environment.NewLine;
                sql += "		        ) as S" + Environment.NewLine;
                sql += "		    group by s.business, s.Wccode,substring(s.wccode,1,1)" + Environment.NewLine;
                sql += "		    ) as z" + Environment.NewLine;
                sql += "		group by z.business,z.division" + Environment.NewLine;
                #region 비지니스 분리전
                //sql += "		SELECT '00' AS Seq,'근무일수' Header2, z.business WcCode, max(z.Sub_Total) Sub_Total" + Environment.NewLine;
                //sql += "		from (" + Environment.NewLine;
                //sql += "		    select s.business, s.Wccode, sum(s.workingday) Sub_Total" + Environment.NewLine;
                //sql += "		    from (" + Environment.NewLine;
                //sql += "		        SELECT t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7) as condate, COUNT(*) as workingday" + Environment.NewLine;
                //sql += "		        FROM (" + Environment.NewLine;
                //sql += "			        SELECT c.Code_Name as business, M.WcCode, M.condate" + Environment.NewLine;
                //sql += "			        FROM BI_Daily_Worker_Management AS M" + Environment.NewLine;
                //sql += "				        LEFT OUTER JOIN BI_WorkCalendar		AS A ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "				        left outer join BI_WorkCenter		as b on m.Wccode = b.wccode" + Environment.NewLine;
                //sql += "				        LEFT OUTER JOIN T_Info_MasterCode	AS c ON c.Major_Code = 'BUSINESS' AND b.Business = c.Minor_Code" + Environment.NewLine;
                //sql += "			        WHERE isnull(B.Business,'') <> '' and M.Wccode <> 'S01' AND (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1'" + Environment.NewLine;
                //sql += "			        GROUP BY c.Code_Name, M.Wccode, M.condate" + Environment.NewLine;
                //sql += "			        ) as T" + Environment.NewLine;
                //sql += "		        GROUP BY t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7)" + Environment.NewLine;
                //sql += "		        ) as S" + Environment.NewLine;
                //sql += "		    group by s.business, s.Wccode" + Environment.NewLine;
                //sql += "		    ) as z" + Environment.NewLine;
                //sql += "		group by z.business" + Environment.NewLine;
                #endregion
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '00' AS Seq,'근무일수' Header2, 'Total' WcCode, max(z.Sub_Total) Sub_Total" + Environment.NewLine;
		        sql += "		from (" + Environment.NewLine;
			    sql += "		    select s.business, s.Wccode, sum(s.workingday) Sub_Total" + Environment.NewLine;
			    sql += "		    from (" + Environment.NewLine;
				sql += "		        SELECT t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7) as condate, COUNT(*) as workingday" + Environment.NewLine;
				sql += "		        FROM (" + Environment.NewLine;
				sql += "			        SELECT c.Code_Name as business, M.WcCode, M.condate" + Environment.NewLine;
				sql += "			        FROM BI_Daily_Worker_Management AS M" + Environment.NewLine;
				sql += "				        LEFT OUTER JOIN BI_WorkCalendar		AS A ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
				sql += "				        left outer join BI_WorkCenter_Matching		as b on m.Wccode = b.wccode" + Environment.NewLine;
				sql += "				        LEFT OUTER JOIN T_Info_MasterCode	AS c ON c.Major_Code = 'BUSINESS' AND b.Business = c.Minor_Code" + Environment.NewLine;
                sql += "			        WHERE isnull(B.Business,'') <> '' and M.Wccode <> 'S01' AND (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1'" + Environment.NewLine;
				sql += "			        GROUP BY c.Code_Name, M.Wccode, M.condate" + Environment.NewLine;
				sql += "			        ) as T" + Environment.NewLine;
				sql += "		        GROUP BY t.business, T.Wccode, substring(convert(varchar, T.condate, 23),1,7)" + Environment.NewLine;
				sql += "		        ) as S" + Environment.NewLine;
			    sql += "		    group by s.business, s.Wccode" + Environment.NewLine;
			    sql += "		    ) as z" + Environment.NewLine;
                sql += "    ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.Work_Count) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "    ) P" + Environment.NewLine;
                sql += "    INNER JOIN ( SELECT '근무일수'  Header_SUB, '기본 근무일수' DisplayMethod, '' Means ) AS A ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                G_DT = SqlDBHelper.FillTable(sql);
                #endregion
                
                #region " 잔업 일수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "FROM (" + Environment.NewLine;
                sql += "	SELECT '01' AS Seq,'잔업일수' Header2, M.WcCode, ISNULL(A.Work_Count,0) Work_Count" + Environment.NewLine;
                sql += "	FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "		LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "			SELECT M.Wccode,COUNT(M.ConDate) AS Work_Count" + Environment.NewLine;
                sql += "			FROM (" + Environment.NewLine;
                sql += "				SELECT M.Wccode,M.ConDate,M.OVERWORKER,A.TOTLAWORKER,CONVERT(numeric,M.OVERWORKER)/CONVERT(numeric,A.TOTLAWORKER)*100 AS OVERRATE" + Environment.NewLine;
                sql += "				FROM (" + Environment.NewLine;
                sql += "					SELECT M.Wccode,M.CONDATE,COUNT(*) AS OVERWORKER" + Environment.NewLine;
                sql += "					FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sql += "						LEFT OUTER JOIN BI_WorkCalendar A	ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "					WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') " + Environment.NewLine;
                sql += "						AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0' AND M.OverTime >= 60 " + Environment.NewLine;
                sql += "					GROUP BY M.Wccode,M.CONDATE" + Environment.NewLine;
                sql += "				) M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "					SELECT M.WCCODE,M.CONDATE,COUNT(*) AS TOTLAWORKER" + Environment.NewLine;
                sql += "					FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sql += "						LEFT OUTER JOIN BI_WorkCalendar A	ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "					WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' " + Environment.NewLine;
                sql += "					GROUP BY M.WCCODE,M.CONDATE" + Environment.NewLine;
                sql += "				) A	ON M.Wccode = A.Wccode AND M.ConDate = A.ConDate" + Environment.NewLine;
                sql += "			) M" + Environment.NewLine;
                sql += "			WHERE M.OVERRATE >= 50" + Environment.NewLine;
                sql += "			GROUP BY M.Wccode" + Environment.NewLine;
                sql += "		) A	ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "	UNION ALL" + Environment.NewLine;
                sql += "	SELECT '01' AS Seq,'잔업일수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.Work_Count,0)) AS FLOAT) / B.W_Qty,0) Sub_Total" + Environment.NewLine;
                sql += "	FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "		LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "			SELECT M.Wccode,COUNT(M.ConDate) AS Work_Count" + Environment.NewLine;
                sql += "			FROM (" + Environment.NewLine;
                sql += "				SELECT M.Wccode,M.ConDate,M.OVERWORKER,A.TOTLAWORKER,CONVERT(numeric,M.OVERWORKER)/CONVERT(numeric,A.TOTLAWORKER)*100 AS OVERRATE" + Environment.NewLine;
                sql += "				FROM (" + Environment.NewLine;
                sql += "					SELECT M.Wccode,M.CONDATE,COUNT(*) AS OVERWORKER" + Environment.NewLine;
                sql += "					FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sql += "						LEFT OUTER JOIN BI_WorkCalendar A	ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "					WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') " + Environment.NewLine;
                sql += "						AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0' AND M.OverTime >= 60 " + Environment.NewLine;
                sql += "					GROUP BY M.Wccode,M.CONDATE" + Environment.NewLine;
                sql += "				) M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "					SELECT M.WCCODE,M.CONDATE,COUNT(*) AS TOTLAWORKER" + Environment.NewLine;
                sql += "					FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sql += "						LEFT OUTER JOIN BI_WorkCalendar A	ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "					WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' " + Environment.NewLine;
                sql += "					GROUP BY M.WCCODE,M.CONDATE" + Environment.NewLine;
                sql += "				) A	ON M.Wccode = A.Wccode AND M.ConDate = A.ConDate" + Environment.NewLine;
                sql += "			) M" + Environment.NewLine;
                sql += "			WHERE M.OVERRATE >= 50" + Environment.NewLine;
                sql += "			GROUP BY M.Wccode" + Environment.NewLine;
                sql += "		) A	ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "			SELECT Z.TeamCode, COUNT(Z.WcCode) W_Qty" + Environment.NewLine;
                sql += "			FROM (" + Environment.NewLine;
                sql += "				SELECT DISTINCT B.TeamCode, M.WcCode" + Environment.NewLine;
                sql += "				FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sql += "					LEFT OUTER JOIN BI_WorkCalendar A	ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "					LEFT OUTER JOIN BI_Team_mapping B	ON M.WcCode = B.WcCode" + Environment.NewLine;
                sql += "				WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') " + Environment.NewLine;
                sql += "					AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0' AND M.OverTime >= 60 " + Environment.NewLine;
                sql += "			) Z" + Environment.NewLine;
                sql += "			GROUP BY Z.TeamCode" + Environment.NewLine;
                sql += "		) B	ON M.TeamCode = B.TeamCode" + Environment.NewLine;
                sql += "		LEFT OUTER JOIN BI_WorkCenter_Matching C	ON C.GUBUN = '01' AND A.Wccode = C.wccode" + Environment.NewLine;
                sql += "		LEFT OUTER JOIN T_Info_MasterCode D	ON D.Major_Code = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "	GROUP BY M.TeamCode, D.Code_Name, B.W_Qty" + Environment.NewLine;
                sql += "	UNION ALL" + Environment.NewLine;
                sql += "	SELECT '01' AS Seq,'잔업일수' Header2, 'Total' WcCode, CONVERT(DECIMAL(20,0),ROUND(M.Work_Count/M.W_Qty,0)) Global_Total" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT CONVERT(DECIMAL,M.Work_Count) AS Work_Count,CONVERT(DECIMAL,M.W_Qty) AS W_Qty" + Environment.NewLine;
                sql += "		FROM (" + Environment.NewLine;
                sql += "			SELECT SUM(R.Work_Count) Work_Count, COUNT(R.Work_Count) W_Qty" + Environment.NewLine;
                sql += "			FROM (" + Environment.NewLine;
                sql += "				SELECT M.Wccode,COUNT(M.ConDate) AS Work_Count" + Environment.NewLine;
                sql += "				FROM (" + Environment.NewLine;
                sql += "					SELECT M.Wccode,M.ConDate,M.OVERWORKER,A.TOTLAWORKER,CONVERT(numeric,M.OVERWORKER)/CONVERT(numeric,A.TOTLAWORKER)*100 AS OVERRATE" + Environment.NewLine;
                sql += "					FROM (" + Environment.NewLine;
                sql += "						SELECT M.Wccode,M.CONDATE,COUNT(*) AS OVERWORKER" + Environment.NewLine;
                sql += "						FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sql += "							LEFT OUTER JOIN BI_WorkCalendar A	ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "						WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') " + Environment.NewLine;
                sql += "							AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0' AND M.OverTime >= 60 " + Environment.NewLine;
                sql += "						GROUP BY M.Wccode,M.CONDATE" + Environment.NewLine;
                sql += "					) M" + Environment.NewLine;
                sql += "					LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "						SELECT M.WCCODE,M.CONDATE,COUNT(*) AS TOTLAWORKER" + Environment.NewLine;
                sql += "						FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sql += "							LEFT OUTER JOIN BI_WorkCalendar A	ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sql += "						WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' " + Environment.NewLine;
                sql += "						GROUP BY M.WCCODE,M.CONDATE" + Environment.NewLine;
                sql += "					) A	ON M.Wccode = A.Wccode AND M.ConDate = A.ConDate" + Environment.NewLine;
                sql += "				) M" + Environment.NewLine;
                sql += "				WHERE M.OVERRATE >= 50" + Environment.NewLine;
                sql += "				GROUP BY M.Wccode" + Environment.NewLine;
                sql += "			) R" + Environment.NewLine;
                sql += "		) M" + Environment.NewLine;
                sql += "	) M" + Environment.NewLine;
                sql += ") Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.Work_Count) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += ") P" + Environment.NewLine;
                sql += "	LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "		SELECT '잔업일수'  Header_SUB, '작업장 잔업일수의 합 ÷ 총 작업장 수' DisplayMethod, '잔업 일수 계산 로직 : 잔업 60분 이상 인원이 작업조 인원의 50% 이상 일 때 잔업 일수 계산' Means" + Environment.NewLine;
                sql += "	) A	ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                #region 수정 전 로직
                //sql += "SELECT *" + Environment.NewLine;
                //sql += "	FROM (" + Environment.NewLine;
                //sql += "		SELECT '01' AS Seq,'야근일수' Header2, M.WcCode, ISNULL(A.Work_Count,0) Work_Count" + Environment.NewLine;
                //sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "								SELECT M.WcCode, COUNT(M.Condate) Work_Count" + Environment.NewLine;
                //sql += "									FROM (" + Environment.NewLine;
                //sql += "										SELECT DISTINCT M.WcCode, M.Condate" + Environment.NewLine;
                //sql += "											FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "													ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "											WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0'" + Environment.NewLine;
                //sql += "                                               AND M.OverTime > 0 " + Environment.NewLine;
                //sql += "										 ) M" + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "								) A" + Environment.NewLine;
                //sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                //sql += "		UNION ALL" + Environment.NewLine;
                //sql += "		SELECT '01' AS Seq,'야근일수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.Work_Count,0)) AS FLOAT) / B.W_Qty,0) Sub_Total" + Environment.NewLine;
                //sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "								SELECT M.WcCode, COUNT(M.Condate) Work_Count" + Environment.NewLine;
                //sql += "									FROM (" + Environment.NewLine;
                //sql += "										SELECT DISTINCT M.WcCode, M.Condate" + Environment.NewLine;
                //sql += "											FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "													ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "											WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0'" + Environment.NewLine;
                //sql += "                                               AND M.OverTime > 0 " + Environment.NewLine;
                //sql += "										 ) M" + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "								) A" + Environment.NewLine;
                //sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "								SELECT Z.TeamCode, COUNT(Z.WcCode) W_Qty" + Environment.NewLine;
                //sql += "									FROM (" + Environment.NewLine;
                //sql += "										SELECT DISTINCT B.TeamCode, M.WcCode" + Environment.NewLine;
                //sql += "											FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "													ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_Team_mapping B" + Environment.NewLine;
                //sql += "													ON M.WcCode = B.WcCode" + Environment.NewLine;
                //sql += "											WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0'" + Environment.NewLine;
                //sql += "                                               AND M.OverTime > 0 " + Environment.NewLine;
                //sql += "										 ) Z" + Environment.NewLine;
                //sql += "									GROUP BY Z.TeamCode" + Environment.NewLine;
                //sql += "								) B" + Environment.NewLine;
                //sql += "					ON M.TeamCode = B.TeamCode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN BI_WorkCenter C" + Environment.NewLine;
                //sql += "					ON C.GUBUN = '01' AND A.Wccode = C.wccode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                //sql += "					ON D.Major_Code = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                //sql += "			GROUP BY M.TeamCode, D.Code_Name, B.W_Qty" + Environment.NewLine;
                //sql += "		UNION ALL" + Environment.NewLine;
                //sql += "		SELECT '01' AS Seq,'야근일수' Header2, 'Total' WcCode, CONVERT(DECIMAL(20,0),ROUND(M.Work_Count/M.W_Qty,0)) Global_Total" + Environment.NewLine;
                //sql += "		FROM (" + Environment.NewLine;
                //sql += "			SELECT CONVERT(DECIMAL,M.Work_Count) AS Work_Count,CONVERT(DECIMAL,M.W_Qty) AS W_Qty" + Environment.NewLine;
                //sql += "           FROM (" + Environment.NewLine;
                //sql += "				SELECT SUM(R.Work_Count) Work_Count, COUNT(R.Work_Count) W_Qty" + Environment.NewLine;
                //sql += "              FROM (" + Environment.NewLine;
                //sql += "	                SELECT M.WcCode, COUNT(M.Condate) Work_Count" + Environment.NewLine;
                //sql += "	                FROM (" + Environment.NewLine;
                //sql += "		                SELECT M.WcCode, M.Condate" + Environment.NewLine;
                //sql += "			            FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "				            LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "					            ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "			            WHERE M.Wccode <> 'S01' AND (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT1' AND M.WorkTime <> '0'" + Environment.NewLine;
                //sql += "                           AND M.OverTime > 0 " + Environment.NewLine;
                //sql += "			            GROUP BY M.Wccode, M.Condate" + Environment.NewLine;
                //sql += "			                 ) M" + Environment.NewLine;
                //sql += "	            GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "	                     ) R" + Environment.NewLine;
                //sql += "	                  ) M" + Environment.NewLine;
                //sql += "               ) M" + Environment.NewLine;
                //sql += "		  ) Z" + Environment.NewLine;
                //sql += "PIVOT (" + Environment.NewLine;
                //sql += "		SUM(z.Work_Count) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                //sql += "	  ) P" + Environment.NewLine;
                //sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "       SELECT '야근일수'  Header_SUB, '야근 일수' DisplayMethod, '' Means" + Environment.NewLine;
                //sql += "                   ) A" + Environment.NewLine;
                //sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                #endregion

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 특별근무 일수 정보 조회 "
                #region 특근일자 기존로직 
                //sql = string.Empty;
                //sql += "SELECT *" + Environment.NewLine;
                //sql += "	FROM (" + Environment.NewLine;
                //sql += "		SELECT '02' AS Seq,'특근일수' Header2, M.WcCode, ISNULL(A.Work_Count,0) Work_Count" + Environment.NewLine;
                //sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "								SELECT M.WcCode, COUNT(M.Condate) Work_Count" + Environment.NewLine;
                //sql += "									FROM (" + Environment.NewLine;
                //sql += "										SELECT DISTINCT M.WcCode, M.Condate" + Environment.NewLine;
                //sql += "											FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "													ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "											WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT3'" + Environment.NewLine;
                //sql += "										 ) M" + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "								) A" + Environment.NewLine;
                //sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                //sql += "		UNION ALL" + Environment.NewLine;
                //sql += "		SELECT '02' AS Seq,'특근일수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.Work_Count,0)) AS FLOAT) / B.W_Qty,0) Sub_Total" + Environment.NewLine;
                //sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "								SELECT M.WcCode, COUNT(M.Condate) Work_Count" + Environment.NewLine;
                //sql += "									FROM (" + Environment.NewLine;
                //sql += "										SELECT DISTINCT M.WcCode, M.Condate" + Environment.NewLine;
                //sql += "											FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "													ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "											WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT3'" + Environment.NewLine;
                //sql += "										 ) M" + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "								) A" + Environment.NewLine;
                //sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "								SELECT Z.TeamCode, COUNT(Z.WcCode) W_Qty" + Environment.NewLine;
                //sql += "									FROM (" + Environment.NewLine;
                //sql += "										SELECT DISTINCT B.TeamCode, M.WcCode" + Environment.NewLine;
                //sql += "											FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "													ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "												LEFT OUTER JOIN BI_Team_mapping B" + Environment.NewLine;
                //sql += "													ON M.WcCode = B.WcCode" + Environment.NewLine;
                //sql += "											WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT3'" + Environment.NewLine;
                //sql += "										 ) Z" + Environment.NewLine;
                //sql += "									GROUP BY Z.TeamCode" + Environment.NewLine;
                //sql += "								) B" + Environment.NewLine;
                //sql += "					ON M.TeamCode = B.TeamCode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN BI_WorkCenter C" + Environment.NewLine;
                //sql += "					ON C.GUBUN = '01' AND A.Wccode = C.wccode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                //sql += "					ON D.Major_Code = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                //sql += "			GROUP BY M.TeamCode, D.Code_Name, B.W_Qty" + Environment.NewLine;
                //sql += "		UNION ALL" + Environment.NewLine;
                //sql += "		SELECT '02' AS Seq,'특근일수' Header2, 'Total' WcCode, CONVERT(DECIMAL(20,0),ROUND(M.Work_Count/M.W_Qty,0)) Global_Total" + Environment.NewLine;
                //sql += "		FROM (" + Environment.NewLine;
                //sql += "			SELECT CONVERT(DECIMAL,M.Work_Count) AS Work_Count,CONVERT(DECIMAL,M.W_Qty) AS W_Qty" + Environment.NewLine;
                //sql += "           FROM (" + Environment.NewLine;
                //sql += "				SELECT SUM(R.Work_Count) Work_Count, COUNT(R.Work_Count) W_Qty" + Environment.NewLine;
                //sql += "              FROM (" + Environment.NewLine;
                //sql += "	                SELECT M.WcCode, COUNT(M.Condate) Work_Count" + Environment.NewLine;
                //sql += "	                FROM (" + Environment.NewLine;
                //sql += "		                SELECT M.WcCode, M.Condate" + Environment.NewLine;
                //sql += "			            FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                //sql += "				            LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                //sql += "					            ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                //sql += "			            WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT3'" + Environment.NewLine;
                //sql += "			            GROUP BY M.Wccode, M.Condate" + Environment.NewLine;
                //sql += "			                 ) M" + Environment.NewLine;
                //sql += "	            GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "	                     ) R" + Environment.NewLine;
                //sql += "	                  ) M" + Environment.NewLine;
                //sql += "               ) M" + Environment.NewLine;
                //sql += "		  ) Z" + Environment.NewLine;
                //sql += "PIVOT (" + Environment.NewLine;
                //sql += "		SUM(z.Work_Count) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                //sql += "	  ) P" + Environment.NewLine;
                //sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "       SELECT '특근일수'  Header_SUB, '특별 근무일수' DisplayMethod, '' Means" + Environment.NewLine;
                //sql += "                   ) A" + Environment.NewLine;
                //sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                #endregion

                string[] sSql = new string[2];
                sSql[0] += "SELECT * INTO #TEMP" + Environment.NewLine;
                sSql[0] += "FROM (" + Environment.NewLine;
                sSql[0] += " SELECT M.WcCode,COUNT(*) AS WorkChk" + Environment.NewLine;
                sSql[0] += " FROM (" + Environment.NewLine;
                sSql[0] += "     SELECT M.workdate,A.wccode,ISNULL(A.LoseTime+ISNULL(B.Default_Qty,0),0) AS WorkChk" + Environment.NewLine;
                sSql[0] += "     FROM (" + Environment.NewLine;
                sSql[0] += "         SELECT WORKDATE " + Environment.NewLine;
                sSql[0] += "         FROM BI_WorkCalendar M" + Environment.NewLine;
                sSql[0] += "         WHERE M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' " + Environment.NewLine;
                sSql[0] += "             AND WORKTYPE = 'WT3'" + Environment.NewLine;
                sSql[0] += "          ) M" + Environment.NewLine;
                sSql[0] += "         LEFT OUTER JOIN (" + Environment.NewLine;
                sSql[0] += "             SELECT M.workdate,M.wccode,ISNULL(SUM(M.LoseTime),0) AS LoseTime" + Environment.NewLine;
                sSql[0] += "             FROM (" + Environment.NewLine;
                sSql[0] += "                 SELECT M.workdate,M.WcCode, ISNULL(A.LoseTime_Total,0) AS LoseTime" + Environment.NewLine;
                sSql[0] += "                 FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sSql[0] += "                     LEFT MERGE JOIN PM_Daily_WorkCenter_Lose A" + Environment.NewLine;
                sSql[0] += "                         ON M.Crno = A.Crno" + Environment.NewLine;
                sSql[0] += "                 WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' )" + Environment.NewLine;
                sSql[0] += "                  ) M" + Environment.NewLine;
                sSql[0] += "             GROUP BY M.workdate,M.wccode" + Environment.NewLine;
                sSql[0] += "             ) A" + Environment.NewLine;
                sSql[0] += "             ON M.workdate = A.workdate" + Environment.NewLine;
                sSql[0] += "         LEFT OUTER JOIN (" + Environment.NewLine;
                sSql[0] += "             SELECT M.workdate,M.WcCode, SUM((A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * D.GQty) Default_Qty" + Environment.NewLine;
                sSql[0] += "             FROM PM_Daily_Worker_Main M" + Environment.NewLine;
                sSql[0] += "                 LEFT OUTER JOIN PM_Daily_Worker_Prod A" + Environment.NewLine;
                sSql[0] += "                     ON M.Wrno = A.Wrno" + Environment.NewLine;
                sSql[0] += "                 INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                sSql[0] += "                     ON A.Itemcode = B.itemcode AND M.opcode IN (B.last_opcode0, B.last_opcode1, B.last_opcode2,  B.last_opcode3)" + Environment.NewLine;
                sSql[0] += "                 LEFT OUTER JOIN BI_ST_Master_Group D" + Environment.NewLine;
                sSql[0] += "                     ON M.OpGroupCode = D.OpGroupCode AND A.ItemCode = D.ItemCode AND A.RQ_ST_DATE = D.Condate" + Environment.NewLine;
                sSql[0] += "             WHERE (M.WorkDate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "') AND SUBSTRING(A.Lotno,6,1) <> '5'" + Environment.NewLine;
                sSql[0] += "             GROUP BY M.workdate,M.WcCode" + Environment.NewLine;
                sSql[0] += "             ) B" + Environment.NewLine;
                sSql[0] += "             ON M.workdate = B.workdate AND A.wccode = B.wccode" + Environment.NewLine;
                sSql[0] += "     ) M" + Environment.NewLine;
                sSql[0] += " WHERE M.WorkChk > 0" + Environment.NewLine;
                sSql[0] += " GROUP BY M.wccode" + Environment.NewLine;
                sSql[0] += " )M" + Environment.NewLine;

                sSql[1] += "SELECT *" + Environment.NewLine;
                sSql[1] += "FROM (" + Environment.NewLine;
                sSql[1] += " SELECT '02' AS Seq,'특근일수' Header2, M.WcCode, ISNULL(A.WorkCnt,0) Work_Count" + Environment.NewLine;
                sSql[1] += " FROM BI_Team_mapping M" + Environment.NewLine;
                sSql[1] += "     LEFT OUTER JOIN (" + Environment.NewLine;
                sSql[1] += "         SELECT M.WCCODE,ISNULL(A.WorkChk,0) AS WorkCnt FROM BI_Team_mapping M" + Environment.NewLine;
                sSql[1] += "             LEFT OUTER JOIN #TEMP A" + Environment.NewLine;
                sSql[1] += "                 ON M.WcCode = A.WcCode" + Environment.NewLine;
                sSql[1] += "         ) A" + Environment.NewLine;
                sSql[1] += "         ON M.WcCode = A.WcCode" + Environment.NewLine;
                sSql[1] += " UNION ALL" + Environment.NewLine;
                sSql[1] += " SELECT '02' AS Seq,'특근일수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.WorkCnt,0)) AS FLOAT) / B.W_Qty,0) Sub_Total" + Environment.NewLine;
                sSql[1] += " FROM BI_Team_mapping M" + Environment.NewLine;
                sSql[1] += "     LEFT OUTER JOIN (" + Environment.NewLine;
                sSql[1] += "         SELECT M.WCCODE,ISNULL(A.WorkChk,0) AS WorkCnt FROM BI_Team_mapping M" + Environment.NewLine;
                sSql[1] += "             LEFT OUTER JOIN #TEMP A" + Environment.NewLine;
                sSql[1] += "                 ON M.WcCode = A.WcCode" + Environment.NewLine;
                sSql[1] += "         ) A" + Environment.NewLine;
                sSql[1] += "         ON M.WcCode = A.WcCode" + Environment.NewLine;
                sSql[1] += "     LEFT OUTER JOIN (" + Environment.NewLine;
                sSql[1] += "         SELECT Z.TeamCode, COUNT(Z.WcCode) W_Qty" + Environment.NewLine;
                sSql[1] += "         FROM (" + Environment.NewLine;
                sSql[1] += "             SELECT DISTINCT B.TeamCode, M.WcCode" + Environment.NewLine;
                sSql[1] += "             FROM BI_Daily_Worker_Management M" + Environment.NewLine;
                sSql[1] += "                 LEFT OUTER JOIN BI_WorkCalendar A" + Environment.NewLine;
                sSql[1] += "                     ON CONVERT(NVARCHAR, Condate, 112) = A.Workdate" + Environment.NewLine;
                sSql[1] += "                 LEFT OUTER JOIN BI_Team_mapping B" + Environment.NewLine;
                sSql[1] += "                     ON M.WcCode = B.WcCode" + Environment.NewLine;
                sSql[1] += "             WHERE (M.Condate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "') AND M.WcCode <> '' AND A.WorkType = 'WT3'" + Environment.NewLine;
                sSql[1] += "              ) Z" + Environment.NewLine;
                sSql[1] += "         GROUP BY Z.TeamCode" + Environment.NewLine;
                sSql[1] += "         ) B" + Environment.NewLine;
                sSql[1] += "         ON M.TeamCode = B.TeamCode" + Environment.NewLine;
                sSql[1] += "     LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sSql[1] += "         ON C.GUBUN = '01' AND A.Wccode = C.wccode" + Environment.NewLine;
                sSql[1] += "     LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sSql[1] += "         ON D.Major_Code = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sSql[1] += " GROUP BY M.TeamCode, D.Code_Name, B.W_Qty" + Environment.NewLine;
                sSql[1] += " UNION ALL" + Environment.NewLine;
                sSql[1] += " SELECT '02' AS Seq,'특근일수' Header2, 'Total' WcCode, CONVERT(DECIMAL(20,0),ROUND(M.Work_Count/M.W_Qty,0)) Global_Total" + Environment.NewLine;
                sSql[1] += " FROM (" + Environment.NewLine;
                sSql[1] += "     SELECT CONVERT(DECIMAL,M.Work_Count) AS Work_Count,CONVERT(DECIMAL,M.W_Qty) AS W_Qty" + Environment.NewLine;
                sSql[1] += "     FROM (" + Environment.NewLine;
                sSql[1] += "         SELECT SUM(WorkChk) Work_Count, COUNT(WorkChk) W_Qty FROM #TEMP ) M" + Environment.NewLine;
                sSql[1] += "     ) M" + Environment.NewLine;
                sSql[1] += " ) Z" + Environment.NewLine;
                sSql[1] += "PIVOT (" + Environment.NewLine;
                sSql[1] += " SUM(z.Work_Count) FOR Z.WcCode IN (" + Header + ")" + Environment.NewLine;
                sSql[1] += " ) P" + Environment.NewLine;
                sSql[1] += " LEFT OUTER JOIN (" + Environment.NewLine;
                sSql[1] += "     SELECT '특근일수'  Header_SUB, '작업장 특근일수의 합 ÷ 총 작업장 수' DisplayMethod, '' Means" + Environment.NewLine;
                sSql[1] += "     ) A" + Environment.NewLine;
                sSql[1] += "     ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                S_DT = SqlDBHelper.FillTable_N("Kortek_TIMS", sSql);
                //S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 재적공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '05' AS Seq,'재적공수' Header2, M.WcCode, ISNULL(A.Default_Qty,0) Default_Qty" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "                               SELECT M.wccode, SUM(A.RQ_worker) Default_Qty" + Environment.NewLine;
                sql += "                               FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                                   LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "                                       ON M.crno = A.crno" + Environment.NewLine;
                sql += "                               WHERE M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                               GROUP BY M.wccode              " + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '05' AS Seq,'재적공수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT),0) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "                               SELECT M.wccode, SUM(A.RQ_worker) Default_Qty" + Environment.NewLine;
                sql += "                               FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                                   LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "                                       ON M.crno = A.crno" + Environment.NewLine;
                sql += "                               WHERE M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                               GROUP BY M.wccode              " + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND A.wccode = C.wccode	" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code	" + Environment.NewLine;
                sql += "			GROUP BY M.TeamCode, D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '05' AS Seq,'재적공수' Header2, 'Total' WcCode, ROUND(CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT),0) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "                               SELECT M.wccode, SUM(A.RQ_worker) Default_Qty" + Environment.NewLine;
                sql += "                               FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                                   LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "                                       ON M.crno = A.crno" + Environment.NewLine;
                sql += "                               WHERE M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                               GROUP BY M.wccode              " + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.Default_Qty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '재적공수'  Header_SUB, '재적인원 X 1일(460分) 정상작업시간' DisplayMethod, '재적인원에 대한 공수를 의미' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 휴업공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '06' AS Seq,'휴업공수' Header2, M.WcCode, ISNULL(A.RestTime,0) RestTime" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "                               SELECT M.wccode, SUM(A.RQ_Rest) RestTime" + Environment.NewLine;
                sql += "                               FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                                   LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "                                       ON M.crno = A.crno" + Environment.NewLine;
                sql += "                               WHERE M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                               GROUP BY M.wccode              " + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '06' AS Seq,'휴업공수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.RestTime,0)) AS FLOAT),0) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "                               SELECT M.wccode, SUM(A.RQ_Rest) RestTime" + Environment.NewLine;
                sql += "                               FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                                   LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "                                       ON M.crno = A.crno" + Environment.NewLine;
                sql += "                               WHERE M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                               GROUP BY M.wccode              " + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND A.wccode = C.wccode	" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "			GROUP BY M.TeamCode, D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '06' AS Seq,'휴업공수' Header2, 'Total' WcCode, ROUND(CAST(SUM(ISNULL(A.RestTime,0)) AS FLOAT),0) Global_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "                               SELECT M.wccode, SUM(A.RQ_Rest) RestTime" + Environment.NewLine;
                sql += "                               FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                                   LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "                                       ON M.crno = A.crno" + Environment.NewLine;
                sql += "                               WHERE M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                               GROUP BY M.wccode              " + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode C" + Environment.NewLine;
                sql += "					ON M.TeamCode = C.Minor_Code AND C.Major_Code = 'TEAM'	" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.RestTime) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '휴업공수'  Header_SUB, '해당인원 X 해당시간' DisplayMethod, '재적인원 중 실제로 작업에 투입되지 않은 인원에 대한 공수 결근,휴가 등' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 휴업율 계산 "
                G_DT.Rows.Add("07", "휴업율");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double break_qty = 0;
                    double Work_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "휴업공수":
                                break_qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "재적공수":
                                Work_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "휴업율":
                                if (Work_Qty == 0.0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((break_qty / Work_Qty) * 100, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "휴업율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "휴업공수 ÷ 재적공수(%)";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 타부서 지원 공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '08' AS Seq,'타부서 지원' Header2, M.WcCode, -1*ISNULL(A.SupportTime,0) SupportTime" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "                                    SELECT M.Wccode, SUM(A.RQ_Support) SupportTime" + Environment.NewLine;
				sql += "                                    FROM PM_daily_WorkCenter_Main M" + Environment.NewLine;
				sql += "	                                    LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
				sql += "		                                    ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "                                    WHERE M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                                    GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '08' AS Seq,'타부서 지원' Header2, D.Code_Name WcCode, -1*ROUND(CAST(SUM(ISNULL(A.SupportTime,0)) AS FLOAT),0) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "                                    SELECT M.Wccode, SUM(A.RQ_Support) SupportTime" + Environment.NewLine;
                sql += "                                    FROM PM_daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "	                                    LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "		                                    ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "                                    WHERE M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                                    GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND A.Wccode =C.wccode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND D.Minor_Code = C.Business" + Environment.NewLine;
                sql += "			GROUP BY M.TeamCode, D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '08' AS Seq,'타부서 지원' Header2, 'Total' WcCode, -1*ROUND(CAST(SUM(ISNULL(A.SupportTime,0)) AS FLOAT),0) Global_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "                                    SELECT M.Wccode, SUM(A.RQ_Support) SupportTime" + Environment.NewLine;
                sql += "                                    FROM PM_daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "	                                    LEFT OUTER JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "		                                    ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "                                    WHERE M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                                    GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.SupportTime) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '타부서 지원'  Header_SUB, '해당인원 X 해당시간' DisplayMethod, '' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 추가공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '09' AS Seq,'추가공수' Header2, M.WcCode, CAST(SUM(ISNULL(B.RQ_Addone,0)) AS FLOAT) AddOne" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM(A.RQ_AddOne) RQ_AddOne" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) B" + Environment.NewLine;
                sql += "					ON M.WcCode = B.WcCode" + Environment.NewLine;
                sql += "			GROUP BY M.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '09' AS Seq,'추가공수' Header2, D.Code_Name WcCode, CAST(SUM(ISNULL(B.RQ_Addone,0)) AS FLOAT) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM(A.RQ_AddOne) RQ_AddOne" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) B" + Environment.NewLine;
                sql += "					ON M.WcCode = B.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND B.wccode = C.wccode	" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "			GROUP BY M.TeamCode, D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '09' AS Seq,'추가공수' Header2,  'Total' WcCode, CAST(SUM(ISNULL(B.RQ_Addone,0)) AS FLOAT) Global_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM(A.RQ_AddOne) RQ_AddOne" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) B" + Environment.NewLine;
                sql += "					ON M.WcCode = B.WcCode" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.AddOne) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '추가공수'  Header_SUB, ' (조별 지원 인원 수)*(재적공수+OT공수)' DisplayMethod, '조별 지원 해준 공수는 (-)값, 조별 지원 받은 공수는 (+)값(1인 공수 기준 : 정상 - 460분, 잔업 - 460분 + OT공수)' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " O/T공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '10' AS Seq,'O/T공수' Header2, M.WcCode, ROUND(CAST(SUM(ISNULL(B.RQ_OT,0)) AS FLOAT),0) RQ_OT" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.RQ_OT),0) AS RQ_OT" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) B" + Environment.NewLine;
                sql += "					ON M.WcCode = B.WcCode" + Environment.NewLine;
                sql += "			GROUP BY M.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '10' AS Seq,'O/T공수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(B.RQ_OT,0)) AS FLOAT),0) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.RQ_OT),0) AS RQ_OT" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) B" + Environment.NewLine;
                sql += "					ON M.WcCode = B.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND B.wccode = C.wccode	" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "			GROUP BY M.TeamCode, D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '10' AS Seq,'O/T공수' Header2,  'Total' WcCode, ROUND(CAST(SUM(ISNULL(B.RQ_OT,0)) AS FLOAT),0) Global_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.RQ_OT),0) AS RQ_OT" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) B" + Environment.NewLine;
                sql += "					ON M.WcCode = B.WcCode" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.RQ_OT) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT 'O/T공수'  Header_SUB, '잔업 + 특근공수' DisplayMethod, '취업공수 이외에 추가로 작업한 공수 잔업,야간' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 작업공수 정보 조회 "
                G_DT.Rows.Add("12","작업공수");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Work_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "재적공수":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "O/T공수":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "휴업공수":
                                Work_Qty -= Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "추가공수":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "타부서 지원":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "작업공수":
                                G_DT.Rows[A][i] = Work_Qty;
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "작업공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "취업공수 + 추가공수 + O/T공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "작업에 투입된 총 공수";
                #endregion

                #region " 유실 공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '13' AS Seq,'유실공수' Header2, M.WcCode, ROUND(CAST(SUM(ISNULL(A.RQ_Lose,0)) AS FLOAT),0) RQ_Lose" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.LoseTime_Total),0) RQ_Lose" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_Lose A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "			GROUP BY M.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '13' AS Seq,'유실공수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.RQ_Lose,0)) AS FLOAT),0) RQ_Lose" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.LoseTime_Total),0) RQ_Lose" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_Lose A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND A.wccode = C.wccode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "			GROUP BY D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '13' AS Seq,'유실공수' Header2, 'Total' WcCode, ROUND(CAST(SUM(ISNULL(A.RQ_Lose,0)) AS FLOAT),0) RQ_Lose" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.LoseTime_Total),0) RQ_Lose" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_Lose A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.RQ_Lose) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '유실공수'  Header_SUB, '해당인원 X 해당시간' DisplayMethod, '비가동공수,자재품절,원자재불량,재 작업 등' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 유실율 정보 조회 "
                G_DT.Rows.Add("14","유실율");
                for (int i = 2; i < G_DT.Columns.Count-3; i++)
                {
                    double Lose_Qty = 0;
                    double Work_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "유실공수":
                                Lose_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "작업공수":
                                Work_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "유실율":
                                if (Work_Qty == 0.0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Lose_Qty / Work_Qty) * 100, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "유실율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "유실공수 ÷ 작업공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 재작업 공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '15' AS Seq,'재작업공수' Header2, M.WcCode, ROUND(CAST(SUM(ISNULL(A.RQ_ReWork,0)) AS FLOAT),0) RQ_ReWork" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.RQ_ReWork),0) RQ_ReWork" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "			GROUP BY M.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '15' AS Seq,'재작업공수' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.RQ_ReWork,0)) AS FLOAT),0) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.RQ_ReWork),0) RQ_ReWork" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND A.wccode = C.wccode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.Major_Code = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "			GROUP BY D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '15' AS Seq,'재작업공수' Header2, 'Total' WcCode, ROUND(CAST(SUM(ISNULL(A.RQ_ReWork,0)) AS FLOAT),0) Global_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, ROUND(SUM(A.RQ_ReWork),0) RQ_ReWork" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_User A" + Environment.NewLine;
                sql += "											ON M.Crno = A.Crno" + Environment.NewLine;
                sql += "									WHERE M.Crno <> '' AND (M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.RQ_ReWork) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '재작업공수'  Header_SUB, '해당인원 X 해당시간' DisplayMethod, '부품품질,ECO 변경, OQC 불합격,Field 불량접수 출하 前 제품' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 실동공수 정보 조회 "
                G_DT.Rows.Add("17","실동공수");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Work_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "작업공수":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "유실공수":
                                Work_Qty -= Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "재작업공수":
                                Work_Qty -= Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동공수":
                                G_DT.Rows[A][i] = Work_Qty;
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "작업공수 - 유실공수 - 재 작업공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "작업공수에서 유실공수를 제외한 공수, 작업에 실질적으로 투입된 공수";
                #endregion

                #region " 조별 AT 합계 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "FROM (" + Environment.NewLine;
                sql += "    SELECT '18' AS Seq,'조별 AT' Header2, M.WcCode, ROUND(CAST(ISNULL(A.SUM_AT,0) AS FLOAT),0)  - ROUND(CAST(ISNULL(B.LOSE_TOTAL,0) AS FLOAT),0)  AS ItemQty" + Environment.NewLine;
                sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "        LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "            SELECT M.WcCode, ISNULL(SUM(A.RQ_AT),0) AS SUM_AT" + Environment.NewLine;
                sql += "            FROM PM_Daily_WorkCenter_Main AS M" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN PM_Daily_WorkCenter_Prod    AS A ON A.gubun = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN BI_Material_Sub             AS B ON B.GUBUN = '01' AND A.Itemcode = B.itemcode" + Environment.NewLine;
                sql += "            WHERE M.GUBUN = '01' AND M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' " + Environment.NewLine;
                sql += "                AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "            GROUP BY M.wccode" + Environment.NewLine;
                sql += "            ) AS A ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "        LEFT OUTER JOIN ( " + Environment.NewLine;
                sql += "                        SELECT M.WCCODE, SUM(A.LOSETIME_TOTAL) AS LOSE_TOTAL" + Environment.NewLine;
                sql += "                        FROM PM_DAILY_WORKCENTER_MAIN AS M" + Environment.NewLine;
                sql += "                           	LEFT OUTER JOIN PM_DAILY_WORKCENTER_LOSE AS A ON M.CRNO = A.CRNO" + Environment.NewLine;
                sql += "                           	LEFT OUTER JOIN BI_Material_Sub             AS B ON B.GUBUN = '01' AND A.Itemcode = B.itemcode" + Environment.NewLine;
                sql += "                        WHERE M.WORKDATE BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' AND ISNULL(A.LOTNO, '') <> ''" + Environment.NewLine;
                sql += "                            AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5'" + Environment.NewLine;
                sql += "                            AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                            AND B.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                sql += "                        GROUP BY M.WCCODE" + Environment.NewLine;
                sql += "        ) AS B ON M.WCCODE = B.WCCODE" + Environment.NewLine;
                sql += "    UNION ALL" + Environment.NewLine;
                sql += "    SELECT '18' AS Seq,'조별 AT' Header2, D.Code_Name WcCode, ROUND(CAST(ISNULL(SUM(A.SUM_AT),0) AS FLOAT),0) - ROUND(CAST(SUM(ISNULL(E.LOSE_TOTAL,0)) AS FLOAT),0) as ItemQty" + Environment.NewLine;
                sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "        LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "            SELECT M.WcCode, ISNULL(SUM(A.RQ_AT),0) AS SUM_AT" + Environment.NewLine;
                sql += "            FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN PM_Daily_WorkCenter_Prod    AS A ON A.gubun = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN BI_Material_Sub             AS B ON B.GUBUN = '01' AND A.Itemcode = B.itemcode" + Environment.NewLine;
                sql += "            WHERE M.GUBUN = '01' AND M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' " + Environment.NewLine;
                sql += "                AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                AND SUBSTRING(A.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //sql += "                AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "            GROUP BY M.wccode" + Environment.NewLine;
                sql += "            ) as A ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "        LEFT OUTER JOIN BI_WorkCenter_Matching       as C ON C.GUBUN = '01' AND A.wccode = C.wccode" + Environment.NewLine;
                sql += "        LEFT OUTER JOIN T_Info_MasterCode   as D ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "        LEFT OUTER JOIN ( " + Environment.NewLine;
                sql += "                        SELECT M.WCCODE, SUM(A.LOSETIME_TOTAL) LOSE_TOTAL" + Environment.NewLine;
                sql += "                        FROM PM_DAILY_WORKCENTER_MAIN AS M" + Environment.NewLine;
                sql += "                        	LEFT OUTER JOIN PM_DAILY_WORKCENTER_LOSE AS A ON M.CRNO = A.CRNO" + Environment.NewLine;
                sql += "                           	LEFT OUTER JOIN BI_Material_Sub             AS B ON B.GUBUN = '01' AND A.Itemcode = B.itemcode" + Environment.NewLine;
                sql += "                        WHERE M.WORKDATE BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' AND ISNULL(A.LOTNO, '') <> ''" + Environment.NewLine;
                sql += "                             AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5'" + Environment.NewLine;
                sql += "                             AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                             AND B.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                sql += "                        GROUP BY M.WCCODE" + Environment.NewLine;
                sql += "                         ) AS E ON M.WCCODE = E.WCCODE" + Environment.NewLine;
                sql += "    GROUP BY D.Code_Name" + Environment.NewLine;
                sql += "    UNION ALL" + Environment.NewLine;
                sql += "    SELECT '18' AS Seq,'조별 AT' Header2, 'Total' WcCode, ROUND(CAST(ISNULL(SUM(A.SUM_AT),0) AS FLOAT),0) - ROUND(CAST(SUM(ISNULL(B.LOSE_TOTAL,0)) AS FLOAT),0) as ItemQty" + Environment.NewLine;
                sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "        LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "            SELECT M.WcCode, ISNULL(SUM(A.RQ_AT),0) AS SUM_AT" + Environment.NewLine;
                sql += "            FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN PM_Daily_WorkCenter_Prod    AS A ON A.gubun = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN BI_Material_Sub             AS B ON B.GUBUN = '01' AND A.Itemcode = B.itemcode" + Environment.NewLine;
                sql += "            WHERE M.GUBUN = '01' AND M.workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' " + Environment.NewLine;
                sql += "                AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "            GROUP BY M.wccode" + Environment.NewLine;
                sql += "            ) as A ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "       LEFT OUTER JOIN ( " + Environment.NewLine;
                sql += "                       SELECT M.WCCODE, SUM(A.LOSETIME_TOTAL) as LOSE_TOTAL" + Environment.NewLine;
                sql += "                       FROM PM_DAILY_WORKCENTER_MAIN AS M" + Environment.NewLine;
                sql += "                            LEFT OUTER JOIN PM_DAILY_WORKCENTER_LOSE AS A ON M.CRNO = A.CRNO" + Environment.NewLine;
                sql += "                            LEFT OUTER JOIN BI_Material_Sub             AS B ON B.GUBUN = '01' AND A.Itemcode = B.itemcode" + Environment.NewLine;
                sql += "                       WHERE M.WORKDATE BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' AND ISNULL(A.LOTNO, '') <> ''" + Environment.NewLine;
                sql += "                             AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5'" + Environment.NewLine;
                sql += "                             AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                             AND B.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                sql += "                       GROUP BY M.WCCODE" + Environment.NewLine;
                sql += "                       ) AS B ON M.WCCODE = B.WCCODE" + Environment.NewLine;
                sql += "    ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "    SUM(z.ItemQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "      ) P" + Environment.NewLine;
                sql += "    LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "        SELECT '조별 AT'  Header_SUB, '' DisplayMethod, '조별 AT 합계' Means" + Environment.NewLine;
                sql += "    ) A" + Environment.NewLine;
                sql += "        ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion 

                if (chk_Visible.Checked == true)
                {
                    #region " 표준공수(1월1일ST) 정보 조회 "
                    sql = string.Empty;
                    sql += "SELECT *" + Environment.NewLine;
                    sql += "	FROM (" + Environment.NewLine;
                    sql += "		SELECT '31' AS Seq,'표준공수(1월1일ST)' Header2, M.WcCode, ROUND(CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT),0) Default_Qty" + Environment.NewLine;
                    sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                    sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                    sql += "								SELECT M.WcCode, ROUND(SUM(CASE WHEN C.GQty IS NULL THEN (A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * D.GQty ELSE (A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * C.GQty END),0) Default_Qty" + Environment.NewLine;
                    sql += "									FROM PM_Daily_Worker_Main M" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN PM_Daily_Worker_Prod A" + Environment.NewLine;
                    sql += "											ON M.Wrno = A.Wrno" + Environment.NewLine;
                    sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                    sql += "											ON A.Itemcode = B.itemcode AND M.opcode IN (B.last_opcode0, B.last_opcode1, B.last_opcode2,  B.last_opcode3)" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN BI_ST_Master_Group C" + Environment.NewLine;
                    sql += "											ON M.OpGroupCode = C.OpGroupCode AND A.ItemCode = C.ItemCode AND SUBSTRING(A.RQ_ST_DATE,1,4) + '0101' = C.Condate" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN BI_ST_Master_Group D" + Environment.NewLine;
                    sql += "											ON M.OpGroupCode = D.OpGroupCode AND A.ItemCode = D.ItemCode AND A.RQ_ST_DATE = D.Condate" + Environment.NewLine;
                    sql += "									WHERE (M.WorkDate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "') AND SUBSTRING(A.Lotno,6,1) <> '5'" + Environment.NewLine;
                    sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                    sql += "								) A" + Environment.NewLine;
                    sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                    sql += "			GROUP BY M.WcCode" + Environment.NewLine;
                    sql += "		UNION ALL" + Environment.NewLine;
                    sql += "		SELECT '31' AS Seq,'표준공수(1월1일ST)' Header2, D.Code_Name WcCode, ROUND(CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT),0) Sub_Total" + Environment.NewLine;
                    sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                    sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                    sql += "								SELECT M.WcCode, ROUND(SUM(CASE WHEN C.GQty IS NULL THEN (A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * D.GQty ELSE (A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * C.GQty END),0) Default_Qty" + Environment.NewLine;
                    sql += "									FROM PM_Daily_Worker_Main M" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN PM_Daily_Worker_Prod A" + Environment.NewLine;
                    sql += "											ON M.Wrno = A.Wrno" + Environment.NewLine;
                    sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                    sql += "											ON A.Itemcode = B.itemcode AND M.opcode IN (B.last_opcode0, B.last_opcode1, B.last_opcode2,  B.last_opcode3)" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN BI_ST_Master_Group C" + Environment.NewLine;
                    sql += "											ON M.OpGroupCode = C.OpGroupCode AND A.ItemCode = C.ItemCode AND SUBSTRING(A.RQ_ST_DATE,1,4) + '0101' = C.Condate" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN BI_ST_Master_Group D" + Environment.NewLine;
                    sql += "											ON M.OpGroupCode = D.OpGroupCode AND A.ItemCode = D.ItemCode AND A.RQ_ST_DATE = D.Condate" + Environment.NewLine;
                    sql += "									WHERE (M.WorkDate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "') AND SUBSTRING(A.Lotno,6,1) <> '5'" + Environment.NewLine;
                    sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                    sql += "								) A" + Environment.NewLine;
                    sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                    sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                    sql += "					ON C.GUBUN = '01' AND A.wccode = C.wccode" + Environment.NewLine;
                    sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                    sql += "					ON D.Major_Code = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                    sql += "			GROUP BY D.Code_Name" + Environment.NewLine;
                    sql += "		UNION ALL" + Environment.NewLine;
                    sql += "		SELECT '31' AS Seq,'표준공수(1월1일ST)' Header2, 'Total' WcCode, ROUND(CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT),0) Global_Total" + Environment.NewLine;
                    sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                    sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                    sql += "								SELECT M.WcCode, ROUND(SUM(CASE WHEN C.GQty IS NULL THEN (A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * D.GQty ELSE (A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * C.GQty END),0) Default_Qty" + Environment.NewLine;
                    sql += "									FROM PM_Daily_Worker_Main M" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN PM_Daily_Worker_Prod A" + Environment.NewLine;
                    sql += "											ON M.Wrno = A.Wrno" + Environment.NewLine;
                    sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                    sql += "											ON A.Itemcode = B.itemcode AND M.opcode IN (B.last_opcode0, B.last_opcode1, B.last_opcode2,  B.last_opcode3)" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN BI_ST_Master_Group C" + Environment.NewLine;
                    sql += "											ON M.OpGroupCode = C.OpGroupCode AND A.ItemCode = C.ItemCode AND SUBSTRING(A.RQ_ST_DATE,1,4) + '0101' = C.Condate" + Environment.NewLine;
                    sql += "										LEFT OUTER JOIN BI_ST_Master_Group D" + Environment.NewLine;
                    sql += "											ON M.OpGroupCode = D.OpGroupCode AND A.ItemCode = D.ItemCode AND A.RQ_ST_DATE = D.Condate" + Environment.NewLine;
                    sql += "									WHERE (M.WorkDate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "') AND SUBSTRING(A.Lotno,6,1) <> '5'" + Environment.NewLine;
                    sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                    sql += "								) A" + Environment.NewLine;
                    sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                    sql += "		  ) Z" + Environment.NewLine;
                    sql += "PIVOT (" + Environment.NewLine;
                    sql += "		SUM(z.Default_Qty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                    sql += "	  ) P" + Environment.NewLine;
                    sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                    sql += "       SELECT '표준공수(1월1일ST)'  Header_SUB, '' DisplayMethod, '' Means" + Environment.NewLine;
                    sql += "                   ) A" + Environment.NewLine;
                    sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                    S_DT = SqlDBHelper.FillTable(sql);
                    G_DT.Merge(S_DT);
                    #endregion
                }

                #region " 표준공수 정보 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '20' AS Seq,'표준공수' Header2, M.WcCode, CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT) Default_Qty" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM((A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * D.GQty) Default_Qty" + Environment.NewLine;
                sql += "									FROM PM_Daily_Worker_Main M" + Environment.NewLine;
                sql += "										LEFT OUTER JOIN PM_Daily_Worker_Prod A" + Environment.NewLine;
                sql += "											ON M.Wrno = A.Wrno" + Environment.NewLine;
                sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                sql += "											ON A.Itemcode = B.itemcode AND M.opcode IN (B.last_opcode0, B.last_opcode1, B.last_opcode2,  B.last_opcode3)" + Environment.NewLine;
                sql += "										LEFT OUTER JOIN BI_ST_Master_Group D" + Environment.NewLine;
                sql += "											ON M.OpGroupCode = D.OpGroupCode AND A.ItemCode = D.ItemCode AND A.RQ_ST_DATE = D.Condate" + Environment.NewLine;
                sql += "									WHERE (M.WorkDate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "') AND SUBSTRING(A.Lotno,6,1) <> '5'" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "			GROUP BY M.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '20' AS Seq,'표준공수' Header2,D.Code_Name WcCode, CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT) Sub_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM((A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * D.GQty) Default_Qty" + Environment.NewLine;
                sql += "									FROM PM_Daily_Worker_Main M" + Environment.NewLine;
                sql += "										LEFT OUTER JOIN PM_Daily_Worker_Prod A" + Environment.NewLine;
                sql += "											ON M.Wrno = A.Wrno" + Environment.NewLine;
                sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                sql += "											ON A.Itemcode = B.itemcode AND M.opcode IN (B.last_opcode0, B.last_opcode1, B.last_opcode2,  B.last_opcode3)" + Environment.NewLine;
                sql += "										LEFT OUTER JOIN BI_ST_Master_Group D" + Environment.NewLine;
                sql += "											ON M.OpGroupCode = D.OpGroupCode AND A.ItemCode = D.ItemCode AND A.RQ_ST_DATE = D.Condate" + Environment.NewLine;
                sql += "									WHERE (M.WorkDate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "') AND SUBSTRING(A.Lotno,6,1) <> '5'" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.gubun = '01' AND A.wccode = C.wccode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.Major_Code = 'BUSINESS' AND D.Minor_Code = C.Business" + Environment.NewLine;
                sql += "			GROUP BY D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '20' AS Seq,'표준공수' Header2, 'Total' WcCode, CAST(SUM(ISNULL(A.Default_Qty,0)) AS FLOAT) Global_Total" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM((A.Q1 + A.Q2 + A.Q3 + A.Q4 + A.Q5) * D.GQty) Default_Qty" + Environment.NewLine;
                sql += "									FROM PM_Daily_Worker_Main M" + Environment.NewLine;
                sql += "										LEFT OUTER JOIN PM_Daily_Worker_Prod A" + Environment.NewLine;
                sql += "											ON M.Wrno = A.Wrno" + Environment.NewLine;
                sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                sql += "											ON A.Itemcode = B.itemcode AND M.opcode IN (B.last_opcode0, B.last_opcode1, B.last_opcode2,  B.last_opcode3)" + Environment.NewLine;
                sql += "										LEFT OUTER JOIN BI_ST_Master_Group D" + Environment.NewLine;
                sql += "											ON M.OpGroupCode = D.OpGroupCode AND A.ItemCode = D.ItemCode AND A.RQ_ST_DATE = D.Condate" + Environment.NewLine;
                sql += "									WHERE (M.WorkDate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "') AND SUBSTRING(A.Lotno,6,1) <> '5'" + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		  ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.Default_Qty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '표준공수'  Header_SUB, 'ST X 1일 생산량' DisplayMethod, '' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                if (chk_Visible.Checked == true)
                {
                    #region " 표준공수 효율(기존ST) 실동공수효율 정보 조회 "
                    G_DT.Rows.Add("32","실동공수효율[기존]");
                    for (int i = 2; i < G_DT.Columns.Count-3; i++)
                    {
                        double Default_Qty = 0;
                        double Real_Qty = 0;

                        for (int A = 0; A < G_DT.Rows.Count; A++)
                        {
                            switch (G_DT.Rows[A]["Header2"].ToString())
                            {
                                case "표준공수(1월1일ST)":
                                    Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                    break;
                                case "실동공수":
                                    Real_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                    break;
                                case "실동공수효율[기존]":
                                    if (Real_Qty == 0.0)
                                    { G_DT.Rows[A][i] = 0; }
                                    else
                                    { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Default_Qty / Real_Qty) * 100, 2); }
                                    break;
                            }
                        }
                    }
                    G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동공수효율[기존]";
                    G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "표준공수(1월1일ST) ÷ 실동공수";
                    G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "실질적으로 투입된 공수중에서 (1월1일ST X 생산량 = 표준공수(1월1일ST)) 비율";
                    #endregion

                    #region " 표준공수 효율(기존ST) 가동율 정보 조회 "
                    G_DT.Rows.Add("33", "실동율[기존]");
                    for (int i = 2; i < G_DT.Columns.Count-3; i++)
                    {
                        double Default_Qty = 0;
                        double Real_Qty = 0;

                        for (int A = 0; A < G_DT.Rows.Count; A++)
                        {
                            switch (G_DT.Rows[A]["Header2"].ToString())
                            {
                                case "실동공수":
                                    Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                    break;
                                case "작업공수":
                                    Real_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                    break;
                                case "실동율[기존]":
                                    if (Real_Qty == 0.0)
                                    { G_DT.Rows[A][i] = 0; }
                                    else
                                    { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Default_Qty / Real_Qty) * 100, 2); }
                                    break;
                            }
                        }
                    }
                    G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동율[기존]";
                    G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "실동공수 ÷ 작업공수";
                    G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "생산의 라인가동상태를 의미. 생산유실의 관리자 책임";
                    #endregion

                    #region " 표준공수 효율(기존ST) 종합생산성 정보 조회 "
                    G_DT.Rows.Add("34", "종합생산성[기존]");
                    for (int i = 2; i < G_DT.Columns.Count-3; i++)
                    {
                        double Default_Qty = 0;
                        double Real_Qty = 0;

                        for (int A = 0; A < G_DT.Rows.Count; A++)
                        {
                            switch (G_DT.Rows[A]["Header2"].ToString())
                            {
                                case "실동공수효율[기존]":
                                    Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                    break;
                                case "실동율":
                                    Real_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                    break;
                                case "종합생산성[기존]":
                                    G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Default_Qty * Real_Qty) / 100, 2);
                                    break;
                            }
                        }
                    }
                    G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "종합생산성[기존]";
                    G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "표준공수 ÷ 작업공수 or 실동공수효율 X 실동율";
                    G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "①시간당 생산능력(실동공수) 향상과 ②라인가동 효율적 관리를 종합적으로 판단, 평가하는 방법으로 실동공수효율과 실동율을 복합지표로 사용";
                    #endregion
                }

                #region " 실동공수효율 정보 조회 "
                G_DT.Rows.Add("21","실동공수효율");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Default_Qty = 0;
                    double Real_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "표준공수":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동공수":
                                Real_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동공수효율":
                                if (Real_Qty == 0.0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Default_Qty / Real_Qty) * 100, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동공수효율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "표준공수 ÷ 실동공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "실질적으로 투입된 공수중에서 (ST X 생산량 = 표준공수) 비율";
                #endregion

                #region " 실동율 정보 조회 "
                G_DT.Rows.Add("22","실동율");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Default_Qty = 0;
                    double Real_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "실동공수":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "작업공수":
                                Real_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동율":
                                if (Real_Qty == 0.0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Default_Qty / Real_Qty) * 100, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "실동공수 ÷ 작업공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "생산의 라인가동상태를 의미. 생산유실의 관리자 책임";
                #endregion

                #region " 종합생산성 정보 조회 "
                G_DT.Rows.Add("23","종합생산성");
                for (int i = 2; i < G_DT.Columns.Count-3; i++)
                {
                    double Default_Qty = 0;
                    double Real_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "실동공수효율":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동율":
                                Real_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "종합생산성":
                                G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Default_Qty * Real_Qty) / 100, 2);
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "종합생산성";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "표준공수 ÷ 작업공수 or 실동공수효율 X 실동율";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "①시간당 생산능력(실동공수) 향상과 ②라인가동 효율적 관리를 종합적으로 판단, 평가하는 방법으로 실동공수효율과 실동율을 복합지표로 사용";
                #endregion

                #region " 계획 수량 조회 "
                string strFrom = dtp_WorkDate_From.Text;
                string strTo = dtp_WorkDate_To.Text;
                sql = string.Empty;
                bool chkDate = strFrom.Substring(5, 2).Equals(strTo.Substring(5, 2)) == true ? true : false;

                string sfdate_1 = string.Empty;
                string stdate_1 = string.Empty;
                string sfdate_2 = string.Empty;
                string stdate_2 = string.Empty;

                if (chkDate)//조회 일자 같은달
                {
                    sfdate_1 = strFrom;
                    stdate_1 = strTo;
                }
                else
                {
                    DateTime stat_1 = dtp_WorkDate_From.DateTime;
                    DateTime end_1 = Convert.ToDateTime(dtp_WorkDate_From.DateTime.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1);
                    DateTime stat_2 = Convert.ToDateTime(dtp_WorkDate_To.Text.Substring(0, 7) + "-01");
                    DateTime end_2 = dtp_WorkDate_To.DateTime;

                    sfdate_1 = stat_1.ToString("yyyy-MM-dd");
                    stdate_1 = end_1.ToString("yyyy-MM-dd");
                    sfdate_2 = stat_2.ToString("yyyy-MM-dd");
                    stdate_2 = end_2.ToString("yyyy-MM-dd");
                }

                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "FROM (" + Environment.NewLine;
                sql += "    SELECT '26' AS Seq,'계획수량' Header2, M.WcCode, 0 PlanQty" + Environment.NewLine;
                sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "    UNION ALL" + Environment.NewLine;
                sql += "	SELECT '26' AS Seq,'계획수량' Header2, ISNULL(A.Code_Name,'') WcCode, M.PlanQty" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "	    SELECT A.B_Group AS BUSINESSCODE, SUM(ISNULL(M.Plan_Qty, 0)) AS PLANQTY" + Environment.NewLine;
		        sql += "	    FROM PM_Weekly_Plan_Confirm AS M" + Environment.NewLine;
			    sql += "	        INNER JOIN BI_Material_Sub AS A ON M.ITEMCODE = A.ITEMCODE" + Environment.NewLine;
                sql += "	    WHERE M.CONDATE BETWEEN '" + sfdate_1 + "' AND '" + stdate_1 + "' AND M.CONDATE = M.CONFIRM_DATE" + Environment.NewLine;
                sql += "	        AND SUBSTRING(M.LOTNO,1,1) <> '5' AND SUBSTRING(M.ITEMCODE,1,1) <> '9' AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                sql += "	    GROUP BY A.B_Group" + Environment.NewLine;
                if (chkDate == false)
                {
                    sql += "	    UNION ALL" + Environment.NewLine;
                    sql += "	    SELECT A.B_Group AS BUSINESSCODE, SUM(ISNULL(M.Plan_Qty, 0)) AS PLANQTY" + Environment.NewLine;
                    sql += "	    FROM PM_Weekly_Plan_Confirm AS M" + Environment.NewLine;
                    sql += "	        INNER JOIN BI_Material_Sub AS A ON M.ITEMCODE = A.ITEMCODE" + Environment.NewLine;
                    sql += "	    WHERE M.CONDATE BETWEEN '" + sfdate_2 + "' AND '" + stdate_2 + "' AND M.CONDATE = M.CONFIRM_DATE" + Environment.NewLine;
                    sql += "	        AND SUBSTRING(M.LOTNO,1,1) <> '5' AND SUBSTRING(M.ITEMCODE,1,1) <> '9' AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                    sql += "	    GROUP BY A.B_Group" + Environment.NewLine;
                }
                sql += "	    ) M" + Environment.NewLine;
                sql += "		LEFT OUTER JOIN T_Info_MasterCode A	ON A.Major_Code = 'BUSINESS' AND M.BUSINESSCODE = A.Minor_Code	" + Environment.NewLine;
                sql += "    UNION ALL" + Environment.NewLine;
                sql += "    SELECT '26' AS Seq,'계획수량' Header2, 'Total' WcCode, ROUND(CAST(ISNULL(SUM(M.PlanQty),0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                sql += "    FROM (" + Environment.NewLine;
                sql += "	    SELECT A.B_Group AS BUSINESSCODE, SUM(ISNULL(M.Plan_Qty, 0)) AS PLANQTY" + Environment.NewLine;
                sql += "	    FROM PM_Weekly_Plan_Confirm AS M" + Environment.NewLine;
                sql += "	        INNER JOIN BI_Material_Sub AS A ON M.ITEMCODE = A.ITEMCODE" + Environment.NewLine;
                sql += "	    WHERE M.CONDATE BETWEEN '" + sfdate_1 + "' AND '" + stdate_1 + "' AND M.CONDATE = M.CONFIRM_DATE" + Environment.NewLine;
                sql += "	        AND SUBSTRING(M.LOTNO,1,1) <> '5' AND SUBSTRING(M.ITEMCODE,1,1) <> '9' AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                sql += "	    GROUP BY A.B_Group" + Environment.NewLine;
                if (chkDate == false)
                {
                    sql += "	    UNION ALL" + Environment.NewLine;
                    sql += "	    SELECT A.B_Group AS BUSINESSCODE, SUM(ISNULL(M.Plan_Qty, 0)) AS PLANQTY" + Environment.NewLine;
                    sql += "	    FROM PM_Weekly_Plan_Confirm AS M" + Environment.NewLine;
                    sql += "	        INNER JOIN BI_Material_Sub AS A ON M.ITEMCODE = A.ITEMCODE" + Environment.NewLine;
                    sql += "	    WHERE M.CONDATE BETWEEN '" + sfdate_2 + "' AND '" + stdate_2 + "' AND M.CONDATE = M.CONFIRM_DATE" + Environment.NewLine;
                    sql += "	        AND SUBSTRING(M.LOTNO,1,1) <> '5' AND SUBSTRING(M.ITEMCODE,1,1) <> '9' AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                    sql += "	    GROUP BY A.B_Group" + Environment.NewLine;
                }
                sql += "	    ) M" + Environment.NewLine;
                sql += ") Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "    SUM(z.PlanQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += ") P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '계획수량'  Header_SUB, '주간 Rolling D+당일 계획확정' DisplayMethod, '' Means" + Environment.NewLine;
                sql += "   ) A	ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                #region " 2017-06-09 최한영부장 요청에 따른 주간 롤링 계획에서 수량 가져오는 로직으로 변경 - [고국현] "
                //sql = string.Empty;
                //sql += "SELECT *" + Environment.NewLine;
                //sql += "FROM (" + Environment.NewLine;
                //sql += "    SELECT '26' AS Seq,'계획수량' Header2, M.WcCode, 0 PlanQty" + Environment.NewLine;
                //sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "    UNION ALL" + Environment.NewLine;
                //sql += "	SELECT '26' AS Seq,'계획수량' Header2, ISNULL(A.Code_Name,'') WcCode, M.PlanQty" + Environment.NewLine;
                //sql += "	FROM (" + Environment.NewLine;
                //sql += "		SELECT M.BUSINESSCODE,ROUND(CAST(ISNULL(SUM(M.PLAN_QTY),0) AS FLOAT),0) AS PLANQTY" + Environment.NewLine;
                //sql += "		FROM (" + Environment.NewLine;
                //sql += "			SELECT M.ITEMCODE,M.Plan_Qty,A.B_Group AS BUSINESSCODE" + Environment.NewLine;
                //sql += "			FROM PM_Monthly_Plan_Confirm M" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN BI_Material_Sub A	ON M.ItemCode =A.itemcode" + Environment.NewLine;
                //sql += "			WHERE M.Condate BETWEEN '" + sfdate_1 + "' AND '" + stdate_1 + "'" + Environment.NewLine;
                //sql += "				AND SUBSTRING(M.LOTNO,1,1) <> '5'" + Environment.NewLine;
                //sql += "				AND SUBSTRING(M.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //sql += "				AND M.MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //sql += "				AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                //if (chkDate == false)
                //{
                //    sql += "			UNION ALL" + Environment.NewLine;
                //    sql += "			SELECT M.ITEMCODE,M.Plan_Qty,A.B_Group AS BUSINESSCODE" + Environment.NewLine;
                //    sql += "			FROM PM_Monthly_Plan_Confirm M" + Environment.NewLine;
                //    sql += "				LEFT OUTER JOIN BI_Material_Sub A	ON M.ItemCode =A.itemcode" + Environment.NewLine;
                //    sql += "			WHERE M.Condate BETWEEN '" + sfdate_2 + "' AND '" + stdate_2 + "'" + Environment.NewLine;
                //    sql += "				AND SUBSTRING(M.LOTNO,1,1) <> '5'" + Environment.NewLine;
                //    sql += "				AND SUBSTRING(M.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //    sql += "				AND M.MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //    sql += "				AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                //}
                //sql += "		) M" + Environment.NewLine;
                //sql += "		GROUP BY M.BUSINESSCODE" + Environment.NewLine;
                //sql += "	) M" + Environment.NewLine;
                //sql += "		LEFT OUTER JOIN T_Info_MasterCode A	ON A.Major_Code = 'BUSINESS' AND M.BUSINESSCODE = A.Minor_Code	" + Environment.NewLine;
                //sql += "    UNION ALL" + Environment.NewLine;
                //sql += "    SELECT '26' AS Seq,'계획수량' Header2, 'Total' WcCode, ROUND(CAST(ISNULL(SUM(M.PlanQty),0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                //sql += "    FROM (" + Environment.NewLine;
                //sql += "		SELECT M.BUSINESSCODE,ROUND(CAST(ISNULL(SUM(M.PLAN_QTY),0) AS FLOAT),0) AS PLANQTY" + Environment.NewLine;
                //sql += "		FROM (" + Environment.NewLine;
                //sql += "			SELECT M.ITEMCODE,M.Plan_Qty,A.B_Group AS BUSINESSCODE" + Environment.NewLine;
                //sql += "			FROM PM_Monthly_Plan_Confirm M" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN BI_Material_Sub A	ON M.ItemCode =A.itemcode" + Environment.NewLine;
                //sql += "			WHERE M.Condate BETWEEN '" + sfdate_1 + "' AND '" + stdate_1 + "'" + Environment.NewLine;
                //sql += "				AND SUBSTRING(M.LOTNO,1,1) <> '5'" + Environment.NewLine;
                //sql += "				AND SUBSTRING(M.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //sql += "				AND M.MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //sql += "				AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                //if (chkDate == false)
                //{
                //    sql += "			UNION ALL" + Environment.NewLine;
                //    sql += "			SELECT M.ITEMCODE,M.Plan_Qty,A.B_Group AS BUSINESSCODE" + Environment.NewLine;
                //    sql += "			FROM PM_Monthly_Plan_Confirm M" + Environment.NewLine;
                //    sql += "				LEFT OUTER JOIN BI_Material_Sub A	ON M.ItemCode =A.itemcode" + Environment.NewLine;
                //    sql += "			WHERE M.Condate BETWEEN '" + sfdate_2 + "' AND '" + stdate_2 + "'" + Environment.NewLine;
                //    sql += "				AND SUBSTRING(M.LOTNO,1,1) <> '5'" + Environment.NewLine;
                //    sql += "				AND SUBSTRING(M.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //    sql += "				AND M.MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //    sql += "				AND A.DayAggregate_Flag <> 'Y'" + Environment.NewLine;
                //}
                //sql += "		) M" + Environment.NewLine;
                //sql += "		GROUP BY M.BUSINESSCODE" + Environment.NewLine;
                //sql += "	) M" + Environment.NewLine;
                //sql += ") Z" + Environment.NewLine;
                //sql += "PIVOT (" + Environment.NewLine;
                //sql += "    SUM(z.PlanQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                //sql += ") P" + Environment.NewLine;
                //sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "       SELECT '계획수량'  Header_SUB, '주간 Rolling D+당일 계획확정' DisplayMethod, '' Means" + Environment.NewLine;
                //sql += "   ) A	ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                #endregion
                #region " 2017-03-02 수정 전 로직 - [박규진] "
                //sql += "SELECT *" + Environment.NewLine;
                //sql += "FROM (" + Environment.NewLine;
                //sql += "    SELECT '25' AS Seq,'계획수량' Header2, M.WcCode, ROUND(CAST(ISNULL(A.PlanQty,0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                //sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "        LEFT MERGE JOIN (" + Environment.NewLine;
                //sql += "            SELECT M.WCCode AS WorkCenter,SUM(M.PlanQty) AS PlanQty" + Environment.NewLine;
                //sql += "            FROM (" + Environment.NewLine;
                //sql += "                SELECT WCCode, SUM(PLAN_QTY) AS PlanQty " + Environment.NewLine;
                //sql += "                FROM PM_Monthly_Plan_Confirm" + Environment.NewLine;
                //sql += "                WHERE Condate BETWEEN '" + sfdate_1 + "' AND '" + stdate_1 + "'" + Environment.NewLine;
                //sql += "                    AND MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //sql += "                    AND SUBSTRING(LOTNO,1,1) <> '5'" + Environment.NewLine;
                //sql += "                    AND SUBSTRING(ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //sql += "                GROUP BY WCCode" + Environment.NewLine;
                //if (chkDate == false)
                //{
                //    sql += "                UNION ALL" + Environment.NewLine;
                //    sql += "                SELECT WCCode, SUM(PLAN_QTY) AS PlanQty " + Environment.NewLine;
                //    sql += "                FROM PM_Monthly_Plan_Confirm" + Environment.NewLine;
                //    sql += "                WHERE Condate BETWEEN '" + sfdate_2 + "' AND '" + stdate_2 + "'" + Environment.NewLine;
                //    sql += "                    AND MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //    sql += "                    AND SUBSTRING(LOTNO,1,1) <> '5'" + Environment.NewLine;
                //    sql += "                    AND SUBSTRING(ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //    sql += "                GROUP BY WCCode" + Environment.NewLine;
                //}
                //sql += "                 ) M" + Environment.NewLine;
                //sql += "            GROUP BY M.WCCode" + Environment.NewLine;
                //sql += "                         ) A" + Environment.NewLine;
                //sql += "            ON M.WcCode = A.WorkCenter" + Environment.NewLine;
                //sql += "    UNION ALL" + Environment.NewLine;
                //sql += "    SELECT '25' AS Seq,'계획수량' Header2, D.Code_Name WcCode, ROUND(CAST(ISNULL(SUM(A.PlanQty),0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                //sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "        LEFT MERGE JOIN (" + Environment.NewLine;
                //sql += "            SELECT M.WCCode AS WorkCenter,SUM(M.PlanQty) AS PlanQty" + Environment.NewLine;
                //sql += "            FROM (" + Environment.NewLine;
                //sql += "                SELECT WCCode, SUM(PLAN_QTY) AS PlanQty " + Environment.NewLine;
                //sql += "                FROM PM_Monthly_Plan_Confirm" + Environment.NewLine;
                //sql += "                WHERE Condate BETWEEN '" + sfdate_1 + "' AND '" + stdate_1 + "'" + Environment.NewLine;
                //sql += "                    AND MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //sql += "                    AND SUBSTRING(LOTNO,1,1) <> '5'" + Environment.NewLine;
                //sql += "                    AND SUBSTRING(ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //sql += "                GROUP BY WCCode" + Environment.NewLine;
                //if (chkDate == false)
                //{
                //    sql += "                UNION ALL" + Environment.NewLine;
                //    sql += "                SELECT WCCode, SUM(PLAN_QTY) AS PlanQty " + Environment.NewLine;
                //    sql += "                FROM PM_Monthly_Plan_Confirm" + Environment.NewLine;
                //    sql += "                WHERE Condate BETWEEN '" + sfdate_2 + "' AND '" + stdate_2 + "'" + Environment.NewLine;
                //    sql += "                    AND MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //    sql += "                    AND SUBSTRING(LOTNO,1,1) <> '5'" + Environment.NewLine;
                //    sql += "                    AND SUBSTRING(ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //    sql += "                GROUP BY WCCode" + Environment.NewLine;
                //}
                //sql += "                 ) M" + Environment.NewLine;
                //sql += "            GROUP BY M.WCCode" + Environment.NewLine;
                //sql += "                         ) A" + Environment.NewLine;
                //sql += "            ON M.WcCode = A.WorkCenter" + Environment.NewLine;
                //sql += "        LEFT OUTER JOIN BI_WorkCenter C" + Environment.NewLine;
                //sql += "            ON C.gubun = '01'AND A.WorkCenter = C.wccode" + Environment.NewLine;
                //sql += "        LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                //sql += "            ON C.Business = D.Minor_Code AND D.Major_Code = 'BUSINESS'" + Environment.NewLine;
                //sql += "    GROUP BY D.Code_Name" + Environment.NewLine;
                //sql += "    UNION ALL" + Environment.NewLine;
                //sql += "    SELECT '25' AS Seq,'계획수량' Header2, 'Total' WcCode, ROUND(CAST(ISNULL(SUM(A.PlanQty),0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                //sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "        LEFT MERGE JOIN (" + Environment.NewLine;
                //sql += "            SELECT M.WCCode AS WorkCenter,SUM(M.PlanQty) AS PlanQty" + Environment.NewLine;
                //sql += "            FROM (" + Environment.NewLine;
                //sql += "                SELECT WCCode, SUM(PLAN_QTY) AS PlanQty " + Environment.NewLine;
                //sql += "                FROM PM_Monthly_Plan_Confirm" + Environment.NewLine;
                //sql += "                WHERE Condate BETWEEN '" + sfdate_1 + "' AND '" + stdate_1 + "'" + Environment.NewLine;
                //sql += "                    AND MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //sql += "                    AND SUBSTRING(LOTNO,1,1) <> '5'" + Environment.NewLine;
                //sql += "                    AND SUBSTRING(ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //sql += "                GROUP BY WCCode" + Environment.NewLine;
                //if (chkDate == false)
                //{
                //    sql += "                UNION ALL" + Environment.NewLine;
                //    sql += "                SELECT WCCode, SUM(PLAN_QTY) AS PlanQty " + Environment.NewLine;
                //    sql += "                FROM PM_Monthly_Plan_Confirm" + Environment.NewLine;
                //    sql += "                WHERE Condate BETWEEN '" + sfdate_2 + "' AND '" + stdate_2 + "'" + Environment.NewLine;
                //    sql += "                    AND MONTHCOUNTFLAG = 'Y'" + Environment.NewLine;
                //    sql += "                    AND SUBSTRING(LOTNO,1,1) <> '5'" + Environment.NewLine;
                //    sql += "                    AND SUBSTRING(ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                //    sql += "                GROUP BY WCCode" + Environment.NewLine;
                //}
                //sql += "                 ) M" + Environment.NewLine;
                //sql += "            GROUP BY M.WCCode" + Environment.NewLine;
                //sql += "                         ) A" + Environment.NewLine;
                //sql += "            ON M.WcCode = A.WorkCenter" + Environment.NewLine;
                //sql += "    ) Z" + Environment.NewLine;
                //sql += "PIVOT (" + Environment.NewLine;
                //sql += "    SUM(z.PlanQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                //sql += "      ) P" + Environment.NewLine;
                //sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "       SELECT '계획수량'  Header_SUB, '월별 경영계획 수량' DisplayMethod, '' Means" + Environment.NewLine;
                //sql += "                   ) A" + Environment.NewLine;
                //sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                #endregion
                #region " 2016-05-03 수정전 - [박규진] "
                //sSQL = string.Empty;
                //sSQL += "SELECT *" + Environment.NewLine;
                //sSQL += "	FROM (" + Environment.NewLine;
                //sSQL += "		SELECT '' Header1, '계획수량' Header2, M.WcCode, ROUND(CAST(ISNULL(A.PlanQty,0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                //sSQL += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sSQL += "				LEFT MERGE JOIN (" + Environment.NewLine;
                //sSQL += "								SELECT WorkCenter, SUM(TQty) PlanQty" + Environment.NewLine;
                //sSQL += "									FROM PM_ProductOrder_Main " + Environment.NewLine;
                //sSQL += "									WHERE PlanDate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "' AND OpGroupCode = '04'" + Environment.NewLine;
                //sSQL += "									GROUP BY WorkCenter" + Environment.NewLine;
                //sSQL += "								) A" + Environment.NewLine;
                //sSQL += "					ON M.WcCode = A.WorkCenter" + Environment.NewLine;
                //sSQL += "		UNION ALL" + Environment.NewLine;
                //sSQL += "		SELECT '' Header1, '계획수량' Header2, C.Code_Name WcCode, ROUND(CAST(ISNULL(SUM(A.PlanQty),0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                //sSQL += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sSQL += "				LEFT MERGE JOIN (" + Environment.NewLine;
                //sSQL += "								SELECT WorkCenter, SUM(TQty) PlanQty" + Environment.NewLine;
                //sSQL += "									FROM PM_ProductOrder_Main " + Environment.NewLine;
                //sSQL += "									WHERE PlanDate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "' AND OpGroupCode = '04'" + Environment.NewLine;
                //sSQL += "									GROUP BY WorkCenter" + Environment.NewLine;
                //sSQL += "								) A" + Environment.NewLine;
                //sSQL += "					ON M.WcCode = A.WorkCenter" + Environment.NewLine;
                //sSQL += "				LEFT OUTER JOIN T_Info_MasterCode C" + Environment.NewLine;
                //sSQL += "					ON M.TeamCode = C.Minor_Code AND C.Major_Code = 'TEAM'" + Environment.NewLine;
                //sSQL += "			GROUP BY C.Code_Name" + Environment.NewLine;
                //sSQL += "		UNION ALL" + Environment.NewLine;
                //sSQL += "		SELECT '' Header1, '계획수량' Header2, 'Total' WcCode, ROUND(CAST(ISNULL(SUM(A.PlanQty),0) AS FLOAT),0) PlanQty" + Environment.NewLine;
                //sSQL += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sSQL += "				LEFT MERGE JOIN (" + Environment.NewLine;
                //sSQL += "								SELECT WorkCenter, SUM(TQty) PlanQty" + Environment.NewLine;
                //sSQL += "									FROM PM_ProductOrder_Main " + Environment.NewLine;
                //sSQL += "									WHERE PlanDate BETWEEN '" + WorkDate_From.ToString("yyyy-MM-dd") + "' AND '" + WorkDate_To.ToString("yyyy-MM-dd") + "' AND OpGroupCode = '04'" + Environment.NewLine;
                //sSQL += "									GROUP BY WorkCenter" + Environment.NewLine;
                //sSQL += "								) A" + Environment.NewLine;
                //sSQL += "					ON M.WcCode = A.WorkCenter" + Environment.NewLine;
                //sSQL += "				LEFT OUTER JOIN T_Info_MasterCode C" + Environment.NewLine;
                //sSQL += "					ON M.TeamCode = C.Minor_Code AND C.Major_Code = 'TEAM'" + Environment.NewLine;
                //sSQL += "		 ) Z" + Environment.NewLine;
                //sSQL += "PIVOT (" + Environment.NewLine;
                //sSQL += "		SUM(z.PlanQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                //sSQL += "	  ) P" + Environment.NewLine;
                #endregion

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 실적 수량 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT '27' AS Seq,'실적수량' Header2, M.WcCode, ROUND(CAST(ISNULL(A.ProdQty,0) AS FLOAT),0) ProdQty" + Environment.NewLine;
                sql += "		FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "			LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "							SELECT M.WcCode, SUM(A.Q1+A.Q2+A.Q3+A.Q4+A.Q5) ProdQty" + Environment.NewLine;
                sql += "							FROM PM_Daily_WorkCenter_Main as M" + Environment.NewLine;
                sql += "								LEFT MERGE JOIN PM_Daily_WorkCenter_Prod as A ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "								INNER JOIN BI_Material_Sub as B ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "							WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                                    AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                                    AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                sql += "                                    AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "							GROUP BY M.WcCode" + Environment.NewLine;
                sql += "							) A" + Environment.NewLine;
                sql += "				ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '27' AS Seq,'실적수량' Header2, D.Code_Name WcCode, ROUND(CAST(ISNULL(SUM(A.ProdQty),0) AS FLOAT),0) ProdQty" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM(A.Q1+A.Q2+A.Q3+A.Q4+A.Q5) ProdQty" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A" + Environment.NewLine;
                sql += "											ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                sql += "											ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "									WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                                       AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                                       AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                sql += "                                       AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN BI_WorkCenter_Matching C" + Environment.NewLine;
                sql += "					ON C.GUBUN = '01' AND A.wccode = C.wccode" + Environment.NewLine;
                sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                sql += "			GROUP BY D.Code_Name" + Environment.NewLine;
                sql += "		UNION ALL" + Environment.NewLine;
                sql += "		SELECT '27' AS Seq,'실적수량' Header2, 'Total' WcCode, ROUND(CAST(ISNULL(SUM(A.ProdQty),0) AS FLOAT),0) ProdQty" + Environment.NewLine;
                sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "								SELECT M.WcCode, SUM(A.Q1+A.Q2+A.Q3+A.Q4+A.Q5) ProdQty" + Environment.NewLine;
                sql += "									FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "										LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A" + Environment.NewLine;
                sql += "											ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                sql += "											ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "									WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                                       AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                                       AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                sql += "                                       AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                sql += "								) A" + Environment.NewLine;
                sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "		 ) Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.ProdQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "	  ) P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '실적수량'  Header_SUB, '생산 실적 수량' DisplayMethod, '' Means" + Environment.NewLine;
                sql += "                   ) A" + Environment.NewLine;
                sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;

                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 실동생산성(시간당) "
                G_DT.Rows.Add("28", "실동생산성(시간당)");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double PROD_QTY = 0;
                    double REAL_QTY = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "실적수량":
                                PROD_QTY = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동공수":
                                REAL_QTY = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동생산성(시간당)":
                                if (PROD_QTY == 0 || REAL_QTY == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust(PROD_QTY / (REAL_QTY / 57.5), 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동생산성(시간당)";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "실적생산수량 ÷ (실동공수 ÷ 57.5分) ";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "시간당 1인당 생산수량(1일 휴무시간 20分제외)";
                #endregion

                #region " 실동생산성(1日) "
                G_DT.Rows.Add("29", "실동생산성(1日)");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double PROD_QTY = 0;
                    double REAL_QTY = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "실적수량":
                                PROD_QTY = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동공수":
                                REAL_QTY = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동생산성(1日)":
                                if (PROD_QTY == 0 || REAL_QTY == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust(PROD_QTY / (REAL_QTY / 460), 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동생산성(1日)";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "실적생산수량 ÷ (실동공수 ÷ 460分)";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "1日 1인당 생산수량";
                #endregion

                #region " 작업생산성(시간당) "
                G_DT.Rows.Add("30", "작업생산성(시간당)");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double PROD_QTY = 0;
                    double REAL_QTY = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "실적수량":
                                PROD_QTY = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "작업공수":
                                REAL_QTY = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "작업생산성(시간당)":
                                if (PROD_QTY == 0 || REAL_QTY == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust(PROD_QTY / (REAL_QTY / 57.5), 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "작업생산성(시간당)";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "실적생산수량 ÷ (작업공수 ÷ 57.5分)";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "(1일 휴무시간 20分제외)";
                #endregion

                #region " 계획 달성율 조회 "
                G_DT.Rows.Add("31","계획 달성율");
                for (int i = 2; i < G_DT.Columns.Count-3; i++)
                {
                    double Plan_Qty = 0;
                    double Prod_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "계획수량":
                                Plan_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실적수량":
                                Prod_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "계획 달성율":
                                if (Prod_Qty == 0 || Plan_Qty == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Prod_Qty * 100) / Plan_Qty, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "계획 달성율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "실적수량 ÷ 계획수량";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 평균계획ST "
                G_DT.Rows.Add("32","평균계획ST");
                for (int i = 2; i < G_DT.Columns.Count-3; i++)
                {
                    double Default_Qty = 0;
                    double Plan_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "표준공수":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "계획수량":
                                Plan_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "평균계획ST":
                                if (Default_Qty == 0 || Plan_Qty == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToRoundDown(Default_Qty / Plan_Qty, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "평균계획ST";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "표준공수 ÷ 계획수량";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 평균실적ST "
                G_DT.Rows.Add("33","평균실적ST");
                for (int i = 2; i < G_DT.Columns.Count-3; i++)
                {
                    double Default_Qty = 0;
                    double Prod_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "표준공수":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실적수량":
                                Prod_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "평균실적ST":
                                if (Default_Qty == 0 && Prod_Qty == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToRoundDown(Default_Qty / Prod_Qty, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "평균실적ST";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "표준공수 ÷ 실적수량";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 평균재적인원 "
                G_DT.Rows.Add("03","평균재적인원");
                double JGROUP_QTY = 0;
                double JTOTAL_QTY = 0;
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Default_Qty = 0;
                    double WorkingDay = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "재적공수":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "근무일수":
                                WorkingDay = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "평균재적인원":
                                if (Default_Qty == 0 && WorkingDay == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                {
                                    if (G_DT.Columns[i].ColumnName.Contains("Total"))
                                    { G_DT.Rows[A][i] = JTOTAL_QTY; }
                                    else if (G_DT.Columns[i].ColumnName.Contains("Gaming") || G_DT.Columns[i].ColumnName.Contains("PID") 
                                        || G_DT.Columns[i].ColumnName.Contains("MEDICAL") || G_DT.Columns[i].ColumnName.Contains("SPL"))
                                    { G_DT.Rows[A][i] = JGROUP_QTY; JGROUP_QTY = 0; }
                                    else
                                    {
                                        if (WorkingDay == 0)
                                        {
                                            G_DT.Rows[A][i] = 0;
                                            JGROUP_QTY += 0;
                                            JTOTAL_QTY += 0;
                                        }
                                        else
                                        {
                                            G_DT.Rows[A][i] = Common_Module.ToHalfAdjust(Default_Qty / WorkingDay / 460, 0);
                                            JGROUP_QTY += Common_Module.ToHalfAdjust(Default_Qty / WorkingDay / 460, 0);
                                            JTOTAL_QTY += Common_Module.ToHalfAdjust(Default_Qty / WorkingDay / 460, 0);
                                        }
                                        
                                    }
                                    
                                }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "평균재적인원";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "(재적공수 ÷ 정상근무일수) ÷ 460分";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 취업공수 정보 조회 "
                G_DT.Rows.Add("08","취업공수");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Work_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "재적공수":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "휴업공수":
                                Work_Qty -= Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "타부서 지원":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "취업공수":
                                G_DT.Rows[A][i] = Work_Qty;
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "취업공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "재적공수 - 휴업공수 - 타부서지원공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "실제로 작업에 투입된 인원에 대한공수";
                #endregion

                #region " O/T율 정보 조회 "
                G_DT.Rows.Add("11", "O/T율");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double ot_Qty = 0;
                    double Work_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "취업공수":
                                Work_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "O/T공수":
                                ot_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "O/T율":
                                if (Work_Qty == 0.0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((ot_Qty / Work_Qty) * 100, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "O/T율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "(잔업 + 특근공수) ÷ 취업공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 평균작업인원 "
                G_DT.Rows.Add("04","평균작업인원");
                double WGROUP_QTY = 0;
                double WTOTAL_QTY = 0;
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Default_Qty = 0;
                    double WorkingDay = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "취업공수":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "근무일수":
                                WorkingDay = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "평균작업인원":
                                if (Default_Qty == 0 && WorkingDay == 0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                {
                                    if (G_DT.Columns[i].ColumnName.Contains("Total"))
                                    { G_DT.Rows[A][i] = WTOTAL_QTY; }
                                    else if (G_DT.Columns[i].ColumnName.Contains("Gaming") || G_DT.Columns[i].ColumnName.Contains("PID")
                                        || G_DT.Columns[i].ColumnName.Contains("MEDICAL") || G_DT.Columns[i].ColumnName.Contains("SPL"))
                                    { G_DT.Rows[A][i] = WGROUP_QTY; WGROUP_QTY = 0; }
                                    else
                                    {
                                        if (WorkingDay == 0)
                                        {
                                            G_DT.Rows[A][i] = 0;
                                            WGROUP_QTY += 0;
                                            WTOTAL_QTY += 0;
                                        }
                                        else
                                        {
                                            G_DT.Rows[A][i] = Common_Module.ToHalfAdjust(Default_Qty / WorkingDay / 460, 0);
                                            WGROUP_QTY += Common_Module.ToHalfAdjust(Default_Qty / WorkingDay / 460, 0);
                                            WTOTAL_QTY += Common_Module.ToHalfAdjust(Default_Qty / WorkingDay / 460, 0);
                                        }
                                        
                                    }

                                }

                                //if (Default_Qty == 0 && WorkingDay == 0)
                                //{ G_DT.Rows[A][i] = 0; }
                                //else
                                //{ G_DT.Rows[A][i] = Common_Module.ToHalfAdjust(Default_Qty / WorkingDay / 460, 0); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "평균작업인원";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "(취업공수 ÷ 정상근무일수) ÷ 460分";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 재작업율 정보 조회 "
                G_DT.Rows.Add("16","재작업율");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Default_Qty = 0;
                    double Real_Qty = 0;

                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "재작업공수":
                                Default_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "작업공수":
                                Real_Qty = Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "재작업율":
                                if (Real_Qty == 0.0)
                                { G_DT.Rows[A][i] = 0; }
                                else
                                { G_DT.Rows[A][i] = Common_Module.ToHalfAdjust((Default_Qty / Real_Qty) * 100, 2); }
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "재작업율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "재작업공수 ÷ 작업공수";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                #region " 실적종수 조회 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "FROM (" + Environment.NewLine;
                sql += "	SELECT '25' AS Seq,'실적종수' Header2, M.WcCode, ROUND(CAST(ISNULL(A.CNT,0) AS FLOAT),0) ItemQty" + Environment.NewLine;
                sql += "	FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "		LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "			SELECT M.WcCode ,COUNT(*) AS CNT" + Environment.NewLine;
                sql += "			FROM (" + Environment.NewLine;
                sql += "				SELECT M.WcCode,A.Itemcode" + Environment.NewLine;
                sql += "				FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "					LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A" + Environment.NewLine;
                sql += "						ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "					INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                sql += "						ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "				WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                    AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                    AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                sql += "                    AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "				GROUP BY M.WcCode,A.Itemcode" + Environment.NewLine;
                sql += "			) M" + Environment.NewLine;
                sql += "			GROUP BY M.WcCode" + Environment.NewLine;
                sql += "		) A	ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "	UNION ALL" + Environment.NewLine;
                sql += "	SELECT '25' AS Seq,'실적종수' Header2, A.Code_Name AS WcCode, ROUND(CAST(ISNULL(M.CNT,0) AS FLOAT),0) ItemQty" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT M.Business,COUNT(M.ITEMCODE) AS CNT" + Environment.NewLine;
                sql += "		FROM (" + Environment.NewLine;
                sql += "			SELECT M.Business,M.Itemcode" + Environment.NewLine;
                sql += "			FROM (" + Environment.NewLine;
                sql += "				SELECT C.Business,M.WcCode,A.Itemcode" + Environment.NewLine;
                sql += "				FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "					LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A	ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "					INNER JOIN BI_Material_Sub B	ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "					INNER JOIN BI_WorkCenter_Matching C	ON M.wccode = C.wccode" + Environment.NewLine;
                sql += "				WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                    AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                    AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                sql += "                    AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "				GROUP BY C.Business,M.WcCode,A.Itemcode" + Environment.NewLine;
                sql += "			) M" + Environment.NewLine;
                sql += "			GROUP BY M.Business,M.Itemcode" + Environment.NewLine;
                sql += "		) M	" + Environment.NewLine;
                sql += "		GROUP BY M.Business" + Environment.NewLine;
                sql += "	) M " + Environment.NewLine;
                sql += "		LEFT OUTER JOIN T_Info_MasterCode A	ON A.Major_Code ='BUSINESS' AND M.Business = A.Minor_Code" + Environment.NewLine;
                sql += "	UNION ALL" + Environment.NewLine;
                sql += "	SELECT '25' AS Seq,'실적종수' Header2, 'Total' WcCode,COUNT(*) AS ItemQty" + Environment.NewLine;
                sql += "	FROM (" + Environment.NewLine;
                sql += "		SELECT DISTINCT A.Itemcode" + Environment.NewLine;
                sql += "		FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                sql += "			LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A	ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "			INNER JOIN BI_Material_Sub B	ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "			INNER JOIN BI_WorkCenter_Matching C	ON M.wccode = C.wccode" + Environment.NewLine;
                sql += "		WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "            AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "            AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                sql += "            AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "	) M" + Environment.NewLine;
                sql += ") Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "		SUM(z.ItemQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += ") P" + Environment.NewLine;
                sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                sql += "       SELECT '실적종수'  Header_SUB, '' DisplayMethod, '해당 조에서 생산한 종수' Means" + Environment.NewLine;
                sql += "   ) A	ON P.Header2 = A.Header_SUB" + Environment.NewLine;


                #region 수정 전 로직-2017-02-22
                //sql += "SELECT *" + Environment.NewLine;
                //sql += "	FROM (" + Environment.NewLine;
                //sql += "		SELECT '24' AS Seq,'품종' Header2, M.WcCode, ROUND(CAST(ISNULL(A.CNT,0) AS FLOAT),0) ItemQty" + Environment.NewLine;
                //sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                //sql += "								SELECT M.WcCode ,COUNT(*) AS CNT" + Environment.NewLine;
                //sql += "								FROM (" + Environment.NewLine;
                //sql += "								    SELECT M.WcCode,A.Itemcode" + Environment.NewLine;
                //sql += "								    FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                //sql += "								        LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A" + Environment.NewLine;
                //sql += "									        ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                //sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                //sql += "											ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                //sql += "									WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                //sql += "                                       AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                //sql += "                                       AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                                       AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode,A.Itemcode" + Environment.NewLine;
                //sql += "									 ) M" + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "								) A" + Environment.NewLine;
                //sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                //sql += "		UNION ALL" + Environment.NewLine;
                //sql += "		SELECT '24' AS Seq,'품종' Header2, D.Code_Name WcCode, ROUND(CAST(ISNULL(SUM(A.CNT),0) AS FLOAT),0) ItemQty" + Environment.NewLine;
                //sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                //sql += "								SELECT M.WcCode ,COUNT(*) AS CNT" + Environment.NewLine;
                //sql += "								FROM (" + Environment.NewLine;
                //sql += "								    SELECT M.WcCode,A.Itemcode" + Environment.NewLine;
                //sql += "								    FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                //sql += "								        LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A" + Environment.NewLine;
                //sql += "									        ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                //sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                //sql += "											ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                //sql += "									WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                //sql += "                                       AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                //sql += "                                       AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                                       AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode,A.Itemcode" + Environment.NewLine;
                //sql += "									 ) M" + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "								) A" + Environment.NewLine;
                //sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN BI_WorkCenter C" + Environment.NewLine;
                //sql += "					ON C.GUBUN = '01' AND A.wccode = C.wccode" + Environment.NewLine;
                //sql += "				LEFT OUTER JOIN T_Info_MasterCode D" + Environment.NewLine;
                //sql += "					ON D.MAJOR_CODE = 'BUSINESS' AND C.Business = D.Minor_Code" + Environment.NewLine;
                //sql += "			GROUP BY D.Code_Name" + Environment.NewLine;
                //sql += "		UNION ALL" + Environment.NewLine;
                //sql += "		SELECT '24' AS Seq,'품종' Header2, 'Total' WcCode, ROUND(CAST(ISNULL(SUM(A.CNT),0) AS FLOAT),0) ItemQty" + Environment.NewLine;
                //sql += "			FROM BI_Team_mapping M" + Environment.NewLine;
                //sql += "				LEFT MERGE JOIN (" + Environment.NewLine;
                //sql += "								SELECT M.WcCode ,COUNT(*) AS CNT" + Environment.NewLine;
                //sql += "								FROM (" + Environment.NewLine;
                //sql += "								    SELECT M.WcCode,A.Itemcode" + Environment.NewLine;
                //sql += "								    FROM PM_Daily_WorkCenter_Main M" + Environment.NewLine;
                //sql += "								        LEFT MERGE JOIN PM_Daily_WorkCenter_Prod A" + Environment.NewLine;
                //sql += "									        ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                //sql += "										INNER JOIN BI_Material_Sub B" + Environment.NewLine;
                //sql += "											ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                //sql += "									WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                //sql += "                                       AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                //sql += "                                       AND SUBSTRING(A.ITEMCODE,1,1) <> '9' " + Environment.NewLine;
                //sql += "                                       AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode,A.Itemcode" + Environment.NewLine;
                //sql += "									 ) M" + Environment.NewLine;
                //sql += "									GROUP BY M.WcCode" + Environment.NewLine;
                //sql += "								) A" + Environment.NewLine;
                //sql += "					ON M.WcCode = A.WcCode" + Environment.NewLine;
                //sql += "		 ) Z" + Environment.NewLine;
                //sql += "PIVOT (" + Environment.NewLine;
                //sql += "		SUM(z.ItemQty) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                //sql += "	  ) P" + Environment.NewLine;
                //sql += "   LEFT OUTER JOIN (" + Environment.NewLine;
                //sql += "       SELECT '품종'  Header_SUB, '' DisplayMethod, '해당 조에서 생산한 종수' Means" + Environment.NewLine;
                //sql += "                   ) A" + Environment.NewLine;
                //sql += "       ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                #endregion
                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region " 평균 인치 "
                sql = string.Empty;
                sql += "SELECT *" + Environment.NewLine;
                sql += "FROM (" + Environment.NewLine;
                sql += "    SELECT '24' AS Seq,'평균 인치' Header2, M.WcCode, ISNULL(A.AverageInch,0) AS AverageInch" + Environment.NewLine;
                sql += "    FROM BI_Team_mapping M" + Environment.NewLine;
                sql += "        LEFT MERGE JOIN (" + Environment.NewLine;
                sql += "            SELECT M.wccode, CASE WHEN SUM(M.ProdQty) = 0 THEN 0 ELSE ROUND(CAST(ISNULL(SUM(M.AverageInch)/SUM(M.ProdQty),0) AS FLOAT),1) END AS AverageInch" + Environment.NewLine;
                sql += "            FROM (" + Environment.NewLine;
                sql += "                SELECT M.workdate,M.wccode,A.Q1+A.Q2+A.Q3+A.Q4+A.Q5 AS ProdQty,ISNULL(CONVERT(FLOAT,C.inch)/10,0) AS Inch, (A.Q1+A.Q2+A.Q3+A.Q4+A.Q5)*ISNULL(CONVERT(FLOAT,C.inch)/10,0) AS AverageInch" + Environment.NewLine;
                sql += "                FROM PM_Daily_WorkCenter_Main as M" + Environment.NewLine;
                sql += "                    LEFT OUTER JOIN PM_Daily_WorkCenter_Prod    as A ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "                    INNER JOIN BI_Material_Sub                  as B ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "                    LEFT OUTER JOIN SAP_INFO.DBO.Info_Material  as C ON A.Itemcode = C.Mcode" + Environment.NewLine;
                sql += "                WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                    AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                    AND SUBSTRING(A.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                sql += "                    AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "             ) as M" + Environment.NewLine;
                sql += "            GROUP BY M.wccode" + Environment.NewLine;
                sql += "            ) as A ON M.WcCode = A.WcCode" + Environment.NewLine;
                sql += "    UNION ALL" + Environment.NewLine;
                sql += "    SELECT '24' AS Seq,'평균 인치' Header2, M.Code_Name WcCode, M.AverageInch" + Environment.NewLine;
                sql += "    FROM (" + Environment.NewLine;
                sql += "        SELECT M.Code_Name, CASE WHEN SUM(M.ProdQty) = 0 THEN 0 ELSE ROUND(CAST(ISNULL(SUM(M.AverageInch)/SUM(M.ProdQty),0) AS FLOAT),1) END AS AverageInch" + Environment.NewLine;
                sql += "        FROM (" + Environment.NewLine;
                sql += "            SELECT M.workdate,M.wccode,E.Code_Name,A.Q1+A.Q2+A.Q3+A.Q4+A.Q5 AS ProdQty,ISNULL(CONVERT(FLOAT,C.inch)/10,0) AS Inch, (A.Q1+A.Q2+A.Q3+A.Q4+A.Q5)*ISNULL(CONVERT(FLOAT,C.inch)/10,0) AS AverageInch" + Environment.NewLine;
                sql += "            FROM PM_Daily_WorkCenter_Main as M" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN PM_Daily_WorkCenter_Prod    as A ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "                INNER JOIN BI_Material_Sub                  as B ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN SAP_INFO.DBO.Info_Material  as C ON A.Itemcode = C.Mcode" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN BI_WorkCenter_Matching               as D ON D.GUBUN = '01' AND M.wccode = D.wccode" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN T_Info_MasterCode           as E ON E.MAJOR_CODE = 'BUSINESS' AND D.Business = E.Minor_Code" + Environment.NewLine;
                sql += "            WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "' " + Environment.NewLine;
                sql += "                AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                AND SUBSTRING(A.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                sql += "                AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "        ) as M" + Environment.NewLine;
                sql += "        GROUP BY M.Code_Name" + Environment.NewLine;
                sql += "    ) M" + Environment.NewLine;
                sql += "    UNION ALL" + Environment.NewLine;
                sql += "    SELECT '24' AS Seq,'평균 인치' Header2, 'Total' WcCode, ISNULL(M.AverageInch,0) AverageInch" + Environment.NewLine;
                sql += "    FROM (" + Environment.NewLine;
                sql += "        SELECT CASE WHEN SUM(M.ProdQty) = 0 THEN 0 ELSE ROUND(CAST(ISNULL(SUM(M.AverageInch)/SUM(M.ProdQty),0) AS FLOAT),1) END AS AverageInch" + Environment.NewLine;
                sql += "        FROM (" + Environment.NewLine;
                sql += "            SELECT M.workdate,M.wccode,A.Q1+A.Q2+A.Q3+A.Q4+A.Q5 AS ProdQty,ISNULL(CONVERT(FLOAT,C.inch)/10,0) AS Inch, (A.Q1+A.Q2+A.Q3+A.Q4+A.Q5)*ISNULL(CONVERT(FLOAT,C.inch)/10,0) AS AverageInch" + Environment.NewLine;
                sql += "            FROM PM_Daily_WorkCenter_Main as M" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN PM_Daily_WorkCenter_Prod    as A ON A.GUBUN = '01' AND M.crno = A.crno" + Environment.NewLine;
                sql += "                INNER JOIN BI_Material_Sub                  as B ON B.GUBUN = '01' AND A.ItemCode = B.ItemCode AND A.OpCode IN (B.Last_OpCode3)" + Environment.NewLine;
                sql += "                LEFT OUTER JOIN SAP_INFO.DBO.Info_Material  as C ON A.Itemcode = C.Mcode" + Environment.NewLine;
                sql += "            WHERE M.GUBUN = '01' AND M.Workdate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "'" + Environment.NewLine;
                sql += "                AND SUBSTRING(RIGHT(A.LOTNO,7),1,1) <> '5' " + Environment.NewLine;
                sql += "                AND SUBSTRING(A.ITEMCODE,1,1) <> '9'" + Environment.NewLine;
                sql += "                AND B.DayAggregate_Flag <> 'Y' " + Environment.NewLine;
                sql += "        ) as M" + Environment.NewLine;
                sql += "    ) as M" + Environment.NewLine;
                sql += "    ) as Z" + Environment.NewLine;
                sql += "PIVOT (" + Environment.NewLine;
                sql += "    SUM(z.AverageInch) FOR Z.WcCode IN ( " + Header + " )" + Environment.NewLine;
                sql += "      ) as P" + Environment.NewLine;
                sql += "    LEFT MERGE JOIN (SELECT '평균 인치'  Header_SUB, '(모델별 인치 X 수량) ÷ 총수량' DisplayMethod, '' Means ) AS A ON P.Header2 = A.Header_SUB" + Environment.NewLine;
                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Merge(S_DT);
                #endregion

                #region 실동공수 vs AT Gap
                G_DT.Rows.Add("19", "실동공수 vs AT Gap");
                for (int i = 2; i < G_DT.Columns.Count - 3; i++)
                {
                    double Work_Qty = 0;
                    double AT_Qty = 0;
                    for (int A = 0; A < G_DT.Rows.Count; A++)
                    {
                        switch (G_DT.Rows[A]["Header2"].ToString())
                        {
                            case "실동공수":
                                Work_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "조별 AT":
                                AT_Qty += Convert.ToDouble(Common_Module.GetNum(G_DT.Rows[A][i].ToString()));
                                break;
                            case "실동공수 vs AT Gap":
                                G_DT.Rows[A][i] = Work_Qty - AT_Qty;
                                break;
                        }
                    }
                }
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "실동공수 vs AT Gap";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "실동 공수 - 조별 AT";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "실동 공수 대비 조별 취합된 공수의 비교";
                #endregion

                #region " 퇴사 인원 "
                sql = string.Empty;
                sql += "		SELECT COUNT(*) as 'CNT' FROM T_Info_User_Status WHERE DI_Type = 8 and (retiredate BETWEEN '" + WorkDate_From.ToString("yyyyMMdd") + "' AND '" + WorkDate_To.ToString("yyyyMMdd") + "')" + Environment.NewLine;
                S_DT = SqlDBHelper.FillTable(sql);
                double retireqty = Convert.ToDouble(S_DT.Rows[0]["CNT"].ToString());
                G_DT.Rows.Add("35", "퇴사인원");
                G_DT.Rows[G_DT.Rows.Count - 1]["Total"] = Convert.ToDouble(S_DT.Rows[0]["CNT"].ToString());
                #endregion

                #region " 퇴사율 "
                sql = string.Empty;
                sql += "    SELECT SUM(A.RQ_worker/460) as worker		" + Environment.NewLine;
                sql += "	FROM PM_Daily_WorkCenter_Main M	    " + Environment.NewLine;
                sql += "	    LEFT OUTER JOIN PM_Daily_WorkCenter_User A	" + Environment.NewLine;
                sql += "	        ON M.crno = A.crno	" + Environment.NewLine;
                sql += "	WHERE M.workdate = DateAdd(DAY, -1, '" + WorkDate_From.ToString("yyyyMMdd") + "')	" + Environment.NewLine;
                S_DT = SqlDBHelper.FillTable(sql);
                G_DT.Rows.Add("36", "퇴사율");
                G_DT.Rows[G_DT.Rows.Count - 1]["Total"] = Common_Module.ToHalfAdjust((retireqty / Convert.ToDouble(S_DT.Rows[0]["worker"].ToString())) * 100, 2);
                G_DT.Rows[G_DT.Rows.Count - 1]["Header_SUB"] = "퇴사율";
                G_DT.Rows[G_DT.Rows.Count - 1]["DisplayMethod"] = "(퇴사 인원수/조회 시작일 이전 근무인원 수)*100%";
                G_DT.Rows[G_DT.Rows.Count - 1]["Means"] = "";
                #endregion

                if (G_DT.Rows.Count > 0)
                {
                    DataView dw = G_DT.DefaultView;
                    dw.Sort = "Seq ASC";
                    SS1.DataSource = dw.ToTable();

                    for (int i = 0; i < SS1_View.RowCount; i++)
                    {
                        DataRow getDR = SS1_View.GetDataRow(i);
                        for (int a = 1; a < SS1_View.Columns.Count; a++)
                        {
                            if (getDR[a].ToString().Equals("0") == true)
                            {
                                getDR[a] = DBNull.Value;
                            }
                        }
                    }

                    
                    //SS1_View.BestFitColumns();
                    ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("조회가 완료 되었습니다.", 0);
                }
                else
                {
                    SS1.DataSource = null;
                    SS1_View.RefreshData();
                    ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("조회 할 데이터가 없습니다.", 1, "조회 할 데이터가 없습니다.");
                }

            }
            catch (Exception ex)
            { ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.", 1, ex.ToString()); }
        }
        #endregion

        #region " Do_Save "
        private void Do_Save()
        {
            try
            {
            }
            catch (Exception ex)
            {
                ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.",1,ex.ToString());
            }
        }
        #endregion

        #region " Do_Excel "
        private void Do_Excel()
        {
            try
            {
                if (SS1_View.RowCount == 0) return;

                using (SaveFileDialog saveDialog = new SaveFileDialog())
                {
                    saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010)(.xlsx)|*.xlsx";
                    //saveDialog.Filter = "Excel (2003)(.xls)|*.xls|Excel (2010) (.xlsx)|*.xlsx |RichText File (.rtf)|*.rtf |Pdf File (.pdf)|*.pdf |Html File (.html)|*.html";
                    if (saveDialog.ShowDialog() != DialogResult.Cancel)
                    {
                        string exportFilePath = saveDialog.FileName;
                        string fileExtenstion = new FileInfo(exportFilePath).Extension;
                        //NImageExporter imageExporter = chartControl.ImageExporter;
                        switch (fileExtenstion)
                        {
                            case ".xls":
                                SS1.ExportToXls(exportFilePath);
                                break;
                            case ".xlsx":
                                SS1.ExportToXlsx(exportFilePath);
                                break;
                            case ".rtf":
                                SS1.ExportToRtf(exportFilePath);
                                break;
                            case ".pdf":
                                SS1.ExportToPdf(exportFilePath);
                                break;
                            case ".html":
                                SS1.ExportToHtml(exportFilePath);
                                break;
                            case ".mht":
                                SS1.ExportToMht(exportFilePath);
                                break;
                            default:
                                break;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.", 1, ex.ToString());
            }
        }
        #endregion

        #region " Do_Delete "
        private void Do_Delete()
        {
            try
            {

            }
            catch (Exception ex)
            { ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.", 1, ex.ToString()); }
        }
        #endregion

        #region " Do_RowNew "
        private void Do_RowNew()
        {
            try
            {

            }
            catch (Exception ex)
            { ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.", 1, ex.ToString()); }
        }
        #endregion

        #region " Do_RowDel "
        private void Do_RowDel()
        {
            try
            {

            }
            catch (Exception ex)
            { ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.", 1, ex.ToString()); }
        }
        #endregion

        #region " Do_New "
        private void Do_New()
        {
            try
            {
                Header = string.Empty;
                dtp_WorkDate_From.Text = DateTime.Now.ToString("yyyy-MM-01");
                dtp_WorkDate_To.Text = DateTime.Now.ToString("yyyy-MM-dd");

                Do_Set_Grid();
            }
            catch (Exception ex)
            { ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.", 1, ex.ToString()); }
            
        }
        #endregion

        #region " 밴드 그리드 헤더 셋팅 함수 "
        private void Do_Set_Grid()
        {
            try
            {
                string sSQL = string.Empty;
                DataTable G_DT = new DataTable();

                var gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBand1.Caption = "구분";
                gridBand1.Name = "Gubun";
                gridBand1.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
                SS1_View.Bands.Add(gridBand1);
                SetGridBand(SS1_View, gridBand1, new string[1] {"Header2" });

                //팀 정보 조회
                sSQL = string.Empty;
                sSQL += "SELECT C.Code_Name as Team, M.WcCode, B.WcName,c.Sort_No" + Environment.NewLine;
                sSQL += "FROM BI_Team_mapping as M" + Environment.NewLine;
                sSQL += "	LEFT OUTER JOIN T_Info_MasterCode   as A ON M.TeamCode = A.Minor_Code AND A.Major_Code = 'TEAM'" + Environment.NewLine;
                sSQL += "	LEFT OUTER JOIN BI_WorkCenter_Matching       as B ON M.WcCode = B.WcCode" + Environment.NewLine;
                sSQL += "	LEFT OUTER JOIN T_Info_MasterCode   as C ON C.Major_Code = 'BUSINESS' AND B.Business = C.Minor_Code" + Environment.NewLine;
                sSQL += "where M.WcCode = B.WcCode" + Environment.NewLine;
                sSQL += "order by C.Sort_No,C.Code_Name, M.WcCode, B.WcName" + Environment.NewLine;
                G_DT = SqlDBHelper.FillTable(sSQL);

                if (G_DT.Rows.Count > 0)
                {
                    DataTable DT_Team = G_DT.DefaultView.ToTable(true, "Team");

                    for (int i = 0; i < DT_Team.Rows.Count; i++)
                    {
                        var gridBand_M = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                        gridBand_M.Caption = DT_Team.Rows[i][0].ToString();
                        gridBand_M.Name = DT_Team.Rows[i][0].ToString();
                        SS1_View.Bands.Add(gridBand_M);

                        DataRow[] DR_WorkCenter = G_DT.Select("Team = '" + DT_Team.Rows[i][0].ToString() + "'");

                        if (DR_WorkCenter.Length > 0)
                        {
                            for (int A = 0; A < DR_WorkCenter.Length; A++)
                            {
                                //각 작업조 
                                var gridBand_1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                                gridBand_1.Caption = DR_WorkCenter[A].ItemArray[2].ToString();
                                gridBand_1.Name = DR_WorkCenter[A].ItemArray[2].ToString();
                                gridBand_1.Width = 100;
                                SS1_View.Bands[DT_Team.Rows[i][0].ToString()].Children.Add(gridBand_1);
                                

                                var gridBand_2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                                gridBand_2.Caption = DR_WorkCenter[A].ItemArray[1].ToString();
                                gridBand_2.Name = DR_WorkCenter[A].ItemArray[1].ToString();
                                gridBand_1.Children.Add(gridBand_2);

                                SetGridBand(SS1_View, gridBand_2, new string[1] { DR_WorkCenter[A].ItemArray[1].ToString() });

                                Header += "[" + DR_WorkCenter[A].ItemArray[1].ToString() + "],";
                            }
                        }

                        //팀 종합
                        var gridBand_3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                        gridBand_3.Caption = DT_Team.Rows[i][0].ToString();
                        gridBand_3.Name = DT_Team.Rows[i][0].ToString();
                        gridBand_3.Width = 100;
                        SS1_View.Bands[DT_Team.Rows[i][0].ToString()].Children.Add(gridBand_3);


                        var gridBand_4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                        gridBand_4.Caption = "소계";
                        gridBand_4.Name = DT_Team.Rows[i][0].ToString();
                        gridBand_3.Children.Add(gridBand_4);

                        SetGridBand(SS1_View, gridBand_4, new string[1] { DT_Team.Rows[i][0].ToString() });

                        Header += "[" + DT_Team.Rows[i][0].ToString() + "],";
                    }

                }

                var gridBand5 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBand5.Caption = "G/Total";
                gridBand5.Name = "Total";
                SS1_View.Bands.Add(gridBand5);
                SetGridBand(SS1_View, gridBand5, new string[1] { "Total"});

                var gridBand6 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBand6.Caption = "산출방식";
                gridBand6.Name = "bDisplayMethod";
                SS1_View.Bands.Add(gridBand6);
                SetGridBand(SS1_View, gridBand6, new string[1] { "DisplayMethod" });

                var gridBand7 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
                gridBand7.Caption = "의미";
                gridBand7.Name = "bMeans";
                SS1_View.Bands.Add(gridBand7);
                SetGridBand(SS1_View, gridBand7, new string[1] { "Means" });

                Header += "[Total]";

                //폰트 및 헤더 정렬
                for (int A = 0; A < SS1_View.Bands.Count; A++)
                {
                    SS1_View.Bands[A].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9);
                    SS1_View.Bands[A].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                    SS1_View.Bands[A].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                    //하위 밴드 컬럼헤드 찾아서 폰트 및 헤더 정렬
                    if (SS1_View.Bands[A].Children.Count > 0)
                    {
                        for (int B = 0; B < SS1_View.Bands[A].Children.Count; B++)
                        {
                            SS1_View.Bands[A].Children[B].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9);
                            SS1_View.Bands[A].Children[B].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                            SS1_View.Bands[A].Children[B].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;

                            if (SS1_View.Bands[A].Children[B].Children.Count > 0)
                            {
                                for (int C = 0; C < SS1_View.Bands[A].Children[B].Children.Count; C++)
                                {
                                    SS1_View.Bands[A].Children[B].Children[C].AppearanceHeader.Font = new System.Drawing.Font("Arial", 9);
                                    SS1_View.Bands[A].Children[B].Children[C].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                                    SS1_View.Bands[A].Children[B].Children[C].AppearanceHeader.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                                    //SS1_View.Bands[A].Children[B].Children[C].Width = 150;
                                }
                            }
                        }
                    }
                }

                for (int i = 0; i < SS1_View.Columns.Count; i++)
                {
                    if (i == 0)
                    {
                        SS1_View.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        SS1_View.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        SS1_View.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        SS1_View.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                        SS1_View.Columns[i].AppearanceCell.Options.UseFont = true;
                    }
                    else if (i < SS1_View.Columns.Count - 2)
                    {
                        SS1_View.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        SS1_View.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                        SS1_View.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        SS1_View.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                        SS1_View.Columns[i].AppearanceCell.Options.UseFont = true;
                    }
                    else
                    {
                        SS1_View.Columns[i].AppearanceCell.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        SS1_View.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
                        SS1_View.Columns[i].AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
                        SS1_View.Columns[i].AppearanceCell.Options.UseTextOptions = true;
                        SS1_View.Columns[i].AppearanceCell.Options.UseFont = true;

                        if (i == SS1_View.Columns.Count - 2)
                        {
                            SS1_View.Columns[i].Width = 300;
                        }
                        else
                        {
                            SS1_View.Columns[i].Width = 800;
                        }
                    }
                }
            }
            catch (Exception ex)
            { ((TIMS_MAIN.Main_Form)this.ParentForm).ShowMsg("에러 메세지가 있습니다.",1,ex.ToString()); }
        }

        //밴드 그리드 컬럼 추가 함수
        private void SetGridBand(DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedView, DevExpress.XtraGrid.Views.BandedGrid.GridBand BandGrid, string[] columnNames)
        {
            int nrOfColumns = columnNames.Length;
            DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] bandedColumns = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[nrOfColumns];
            for (int i = 0; i < nrOfColumns; i++)
            {
                bandedColumns[i] = (DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn)bandedView.Columns.AddField(columnNames[i]);
                bandedColumns[i].OptionsColumn.AllowEdit = false;
                bandedColumns[i].OptionsColumn.ReadOnly = true;
                bandedColumns[i].OwnerBand = BandGrid;
                bandedColumns[i].Visible = true;
            }
        }
        #endregion

    }
}