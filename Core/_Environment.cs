using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace Core
{
    /// <summary>
    /// 전체적인 환경 설정을 가지고 있는 클래스입니다.
    /// </summary>
    public class _Environment : INotifyPropertyChanged
    {
        //private uint _String;

        string _server;
        string _database;
        string _dbid;
        string _password;
        
        string _empname;
        string _empcode;
        string _dept;
        string _Factory;
        string _Company;
        string _FactoryName;
        string _CompanyName;
        string _MyIP;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler ServerChanged;
        public event EventHandler DatabaseChanged;
        public event EventHandler DbidChanged;
        public event EventHandler PasswordChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void OnServerChanged()
        {
            if (ServerChanged != null)
            {
                ServerChanged(this, new EventArgs());
            }
        }

        private void OnDatabaseChanged()
        {
            if (DatabaseChanged != null)
            {
                DatabaseChanged(this, new EventArgs());
            }
        }
        private void OnDbidChanged()
        {
            if (DbidChanged != null)
            {
                DbidChanged(this, new EventArgs());
            }
        }
        private void OnPasswordChanged()
        {
            if (PasswordChanged != null)
            {
                PasswordChanged(this, new EventArgs());
            }
        }

        public string MyIP
        {
            get { return _MyIP; }
            set
            {

                if (value != this._MyIP)
                {
                    _MyIP = value;
                    NotifyPropertyChanged("MyIP");
                }

            }
        }

        public string Server
        {
            get { return _server; }
            set
            {

                if (value != this._server)
                {
                    _server = value;
                    NotifyPropertyChanged("Server");
                    OnServerChanged();
                }

            }
        }

        public string Database
        {
            get { return _database; }
            set
            {

                if (value != this._database)
                {
                    _database = value;
                    NotifyPropertyChanged("Database");
                    OnDatabaseChanged();
                }

            }
        }

        public string Dbid
        {
            get { return _dbid; }
            set
            {

                if (value != this._dbid)
                {
                    _dbid = value;
                    NotifyPropertyChanged("Dbid");
                    OnDbidChanged();
                }

            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (value != this._password)
                {
                    _password = value;
                    NotifyPropertyChanged("Password");
                    OnPasswordChanged();
                }

            }
        }
        
        public string EmpName
        {
            get { return _empname; }
            set
            {
                if (value != this._empname)
                {
                    _empname = value;
                    NotifyPropertyChanged("EmpName");
                }
            }
        }

        public string EmpCode
        {
            get { return _empcode; }
            set
            {
                if (value != this._empcode)
                {
                    _empcode = value;
                    NotifyPropertyChanged("EmpCode");
                }
            }

        }

        public string Dept
        {
            get { return _dept; }
            set
            {
                if (value != this._dept)
                {
                    _dept = value;
                    NotifyPropertyChanged("Dept");
                }
            }

        }
        public string Factory
        {
            get { return _Factory; }
            set
            {
                _Factory = value;
                NotifyPropertyChanged("Factory");

            }

        }

        public string Company
        {
            get { return _Company; }
            set
            {

                _Company = value;
                NotifyPropertyChanged("Company");
            }

        }
        public string FactoryName
        {
            get { return _FactoryName; }
            set
            {
                _FactoryName = value;
                NotifyPropertyChanged("FactoryName");

            }

        }

        public string CompanyName
        {
            get { return _CompanyName; }
            set
            {

                _CompanyName = value;
                NotifyPropertyChanged("CompanyName");
            }

        }

        public string DBConString
        {
            get
            {
                return "Server =" + Server + ";Initial Catalog =" + Database + ";User Id = " + Dbid + ";Password = " + Password;
            }
        }

        public _Environment()
        {
            Server = Properties.Settings.Default.SERVER_IP;
            Database = Properties.Settings.Default.DB;
            Dbid = Properties.Settings.Default.SERVER_ID;
            Password = Properties.Settings.Default.SERVER_PWD;

            EmpCode = Properties.Settings.Default.EMP_Code;
            EmpName = Properties.Settings.Default.EMP_Name;
            Dept = Properties.Settings.Default.DEPT;
            Company = Properties.Settings.Default.COMP;
            Factory = Properties.Settings.Default.FACT;
            MyIP = Properties.Settings.Default.MyIP;
        }

        public void Load()
        {
            Properties.Settings.Default.Reload();
            Server = Properties.Settings.Default.SERVER_IP;
            Database = Properties.Settings.Default.DB;
            Dbid = Properties.Settings.Default.SERVER_ID;
            Password = Properties.Settings.Default.SERVER_PWD;

            EmpCode = Properties.Settings.Default.EMP_Code;
            EmpName = Properties.Settings.Default.EMP_Name;
            Dept = Properties.Settings.Default.DEPT;
            Company = Properties.Settings.Default.COMP;
            Factory = Properties.Settings.Default.FACT;
            MyIP = Properties.Settings.Default.MyIP;
        }

        public void Default()
        {
            Properties.Settings.Default.Reset();
            Load();
        }

        public void Save()
        {
            Properties.Settings.Default.SERVER_IP = _server;
            Properties.Settings.Default.DB = _database;
            Properties.Settings.Default.SERVER_ID = _dbid;
            Properties.Settings.Default.SERVER_PWD = _password;

            Properties.Settings.Default.EMP_Code = _empcode;
            Properties.Settings.Default.EMP_Name = _empname;
            Properties.Settings.Default.DEPT = _dept;
            Properties.Settings.Default.COMP = _Company;
            Properties.Settings.Default.FACT = _Factory;
            Properties.Settings.Default.MyIP = _MyIP;
            Properties.Settings.Default.Save();
        }
    }
}


