using System.Collections.Generic;
using System.Text;

namespace TaskManagement.DataTransformator
{
    public class DbManagementRow
    {
        private readonly Dictionary<string, bool> _skills;

        public DbManagementRow()
        {
            _skills = new Dictionary<string, bool> {
                                                       {WcfProgramming, false},
                                                       {WpfProgramming, false},
                                                       {WfProgramming, false},
                                                       {MsSqlServer, false},
                                                       {OracleDatabase, false},
                                                       {JavaScriptProgramming, false},
                                                       {CssFundamentals, false},
                                                       {OpcFundamentals, false},
                                                       {SilverlightFundamentals, false},
                                                       {AspNetFundamentals, false},
                                                       {LocalizationFundamentals, false},
                                                       {ArchitectureFundamentals, false},
                                                       {PerformanceFundamentals, false},
                                                       {ComFundamentals, false},
                                                       {RefactoringFundamentals, false},
                                                       {BuildServer, false},
                                                       {VbNetProgramming, false},
                                                       {Sharepoint, false},
                                                       {IISAdministrationFundamentals, false},
                                                       {BizTalkServerAdministrationFundamentals, false},
                                                       {XmlFrameworkFundamentals, false}
                                                   };
        }

        public Dictionary<string, bool> Skills { get { return _skills; }}

        public AreaName Area { get; set; }

        public const string WcfProgramming = "WCF Programming";
        public const string WpfProgramming = "WPF Programming";
        public const string WfProgramming = "WF Programming";
        public const string MsSqlServer = "Ms Sql Server";
        public const string OracleDatabase = "Oracle Database";
        public const string JavaScriptProgramming = "JavaScript Programming";
        public const string CssFundamentals = "CSS Fundamentals";
        public const string OpcFundamentals = "OPC Fundamentals";
        public const string SilverlightFundamentals = "Silverlight Fundamentals";
        public const string AspNetFundamentals = "ASP.NET Fundamentals";
        public const string LocalizationFundamentals = "Localization Fundamentals";
        public const string ArchitectureFundamentals = "Architecture Fundamentals";
        public const string PerformanceFundamentals = "Performance Fundamentals";
        public const string ComFundamentals = "COM Fundamentals";
        public const string RefactoringFundamentals = "Refactoring Fundamentals";
        public const string BuildServer = "Build Server";
        public const string VbNetProgramming = "VB.net Programming";
        public const string Sharepoint = "Sharepoint";
        public const string IISAdministrationFundamentals = "IIS Administration Fundamentals";
        public const string BizTalkServerAdministrationFundamentals = "BizTalk Server Administration Fundamentals";
        public const string XmlFrameworkFundamentals = "XML Framework Fundamentals";

        public string Employee { get; set; }

        public string HeaderRow
        {
            get
            {
                var header = new StringBuilder();

                header.Append("Area;");

                foreach (string skill in this.Skills.Keys)
                    header.Append(string.Format("{0};", skill));

                header.Append("Employee");

                return header.ToString();
            }
        }

        public override string ToString()
        {
            var taskRow = new StringBuilder();

            taskRow.Append(string.Format("{0};", Area));

            foreach (string skill in this.Skills.Keys)
                taskRow.Append(string.Format("{0};", Skills[skill]));

            taskRow.Append(Employee);

            return taskRow.ToString();
        }
    }
}