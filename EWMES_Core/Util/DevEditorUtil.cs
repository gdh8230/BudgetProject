using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;

namespace DH_Core
{
    public class DevEditorUtil
    {
        /// <summary>
        /// DevExpress Editor SearchlookUpEditor 세팅
        /// </summary>
        /// <param name="lue">룩업에디트</param>
        /// <param name="dt">데이터테이블</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">값 이름</param>
        public static void DevLookUpEditorSet(SearchLookUpEdit lue, DataTable dt, String key, String value)
        {
            try
            {
                lue.Properties.DataSource = null;
                lue.Properties.DataSource = dt;
                lue.Properties.DisplayMember = key;
                lue.Properties.ValueMember = value;
                //lue.Properties.Properties.ShowHeader = false;
            }
            catch
            {

                lue.Properties.DataSource = null;
            }
        }
        /// <summary>
        /// DevExpress Editor lookUpEditor 세팅
        /// </summary>
        /// <param name="lue">룩업에디트</param>
        /// <param name="dt">데이터테이블</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">값 이름</param>
        public static void DevLookUpEditorSet(LookUpEdit lue, DataTable dt, String key, String value)
        {
            try
            {
                lue.Properties.DataSource = null;
                lue.Properties.DataSource = dt;
                lue.Properties.DisplayMember = key;
                lue.Properties.ValueMember = value;
                //lue.Properties.Properties.ShowHeader = false;
            }
            catch
            {
                lue.Properties.DataSource = null;
            }
        }

        public static void DevLookUpEditorSet(LookUpEdit lue, DataView dv, String key, String value)
        {
            try
            {
                lue.Properties.DataSource = null;
                lue.Properties.DataSource = dv;
                lue.Properties.DisplayMember = key;
                lue.Properties.ValueMember = value;
                //lue.Properties.Properties.ShowHeader = false;
            }
            catch
            {

                lue.Properties.DataSource = null;
            }
        }

        /// <summary>
        /// DevExpress Editor lookUpEditor 세팅
        /// </summary>
        /// <param name="lue">룩업에디트</param>
        /// <param name="dt">데이터테이블</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">값 이름</param>
        public static void DevLookUpEditorSet(RepositoryItemLookUpEdit RepLookupEdit, DataTable dt, String key, String value, String keytext, String valuetext)
        {
            try
            {
                RepLookupEdit.DataSource = null;
                RepLookupEdit.DataSource = dt;
                RepLookupEdit.Columns.Clear();
                RepLookupEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(key, keytext));
                RepLookupEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value, valuetext));
                RepLookupEdit.DisplayMember = value;
                RepLookupEdit.ValueMember = key;
                RepLookupEdit.ShowHeader = false;
            }
            catch
            {

                RepLookupEdit.DataSource = null;
            }
        }
        /// <summary>
        /// DevExpress Editor lookUpEditor 세팅
        /// </summary>
        /// <param name="lue">룩업에디트</param>
        /// <param name="dv">데이터뷰</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">값 이름</param>
        public static void DevLookUpEditorSet(RepositoryItemLookUpEdit RepLookupEdit, DataView dv, String key, String value, String keytext, String valuetext)
        {
            try
            {
                RepLookupEdit.DataSource = null;
                RepLookupEdit.DataSource = dv;
                RepLookupEdit.Columns.Clear();
                RepLookupEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(key, keytext));
                RepLookupEdit.Columns.Add(new DevExpress.XtraEditors.Controls.LookUpColumnInfo(value, valuetext));
                RepLookupEdit.DisplayMember = value;
                RepLookupEdit.ValueMember = key;
                RepLookupEdit.ShowHeader = false;
            }
            catch
            {

                RepLookupEdit.DataSource = null;
            }
        }
        /// <summary>
        /// DevExpress Editor lookUpEditor 세팅
        /// </summary>
        /// <param name="lue">룩업에디트</param>
        /// <param name="dt">데이터테이블</param>
        /// <param name="key">키 이름</param>
        /// <param name="value">값 이름</param>
        public static void DevLookUpEditorSet(LookUpEdit lue, List<KeyValuePair<String, String>> listKeyValuePair)
        {
            try
            {
                lue.Properties.DataSource = null;
                lue.Properties.DataSource = listKeyValuePair;
                lue.Properties.DisplayMember = "Key";
                lue.Properties.ValueMember = "Value";
            }
            catch
            {
                lue.Properties.DataSource = null;
            }
        }

        /// <summary>
        /// XtraEditors.Repository.RepositoryItemLookUpEdit 의 리스트 삽입
        /// </summary>
        /// <param name="LookupEdit">컨트롤</param>
        /// <param name="list">KeyValuePair리스트</param>
        /// <param name="isGridIn"> </param>
        public static void DevRepositoryLookUpEditorSet(RepositoryItemLookUpEdit RepLookupEdit, List<KeyValuePair<String, String>> list)
        {
            RepLookupEdit.DataSource = null;
            RepLookupEdit.DataSource = list;
            RepLookupEdit.DisplayMember = "Key";
            RepLookupEdit.ValueMember = "Value";
        }
    }
}
