using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudAwesome.PortalTestFramework.Models
{
    public static class SupportPage
    {
        public static string PageUrl => "support";
        public static string CreateNewCaseUrl => "/support/create-case/";
        public static string CreateNewCase => "Open a New Case";
        public static string Title => "title";
        public static string Customer => "customerid";
        public static string CaseType => "casetypecode";
        public static string Subject => "subjectid";
        public static string Description => "description";
        public static string Submit => "InsertButton";
        public static string Cancel => "Cancel";

    }
}
