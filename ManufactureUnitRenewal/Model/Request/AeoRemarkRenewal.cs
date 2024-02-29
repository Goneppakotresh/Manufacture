using Microsoft.AspNetCore.Mvc.Rendering;

namespace IEMS_WEB.Areas.ManufactureUnitRenewal.Model.Request
{
    public class AeoRemarkRenewal
    {
       
        public int RequestId { get; set; }
        public int TotalInsepections { get; set; }
        public string DateOfInsepections { get; set; } = string.Empty;
        public string DetailsOFDiscrepancy { get; set; } = string.Empty;
        public int IsLabPlantWorking { get; set; }
        public int IsWebCamerasWorking { get; set; }
        public int PendingYearId { get; set; }
        public string PendingAmount { get; set; } = string.Empty;
        public string FIRNo { get; set; } = string.Empty;
        public string FIRDate { get; set; } = string.Empty;
        public int FIRStatus { get; set; }
        public int CourtStay { get; set; }
        public int NoOfCases { get; set; }
        public string RemarkOfCases { get; set; } = string.Empty;
        public string ActionDetailAgainstUnit { get; set; } = string.Empty;
        public string RemarksBasedOnApplication { get; set; } = string.Empty;
        public string InsepectionsJson { get; set; } = string.Empty;
        public string PendingDuesJson { get; set; } = string.Empty;
        public string CasesJson { get; set; } = string.Empty;

        public List<SelectListItem> lstYears { get; set; } = new List<SelectListItem>();
        public List<AEO> lstAeoDetail { get; set; } = new List<AEO>();
        public List<AEOInsection> lstInsection { get; set; } = new List<AEOInsection>();
        public List<AEOPendingDues> lstPendingDues { get; set; } = new List<AEOPendingDues>();
        public List<AEOFIR> lstFir { get; set; } = new List<AEOFIR>();
    }
    public class AEO
    {
        public int TotalInsection { get; set; }
        public int IsLab { get; set; }
        public int ISWeb { get; set; }
        public int NoOfCAses { get; set; }
        public string Remark { get; set; }
        public string ActionAgaistUnit { get; set; }
        public string ReamrkBasedOnApplication { get; set; }
    }
    public class AEOInsection
    {
        public string Date { get; set; }
        public string Detail { get; set; }
    }
    public class AEOPendingDues
    {
        public string Year { get; set; }
        public string Amount { get; set; }
    }
    public class AEOFIR
    {
        public string FirNo { get; set; }
        public string Date { get; set; }
        public string status { get; set; }
        public string CourtStay { get; set; }
    }
    public class DeoRemarkRenewal
    {
        public int RequestId { get; set; }
        public int TotalInsepections { get; set; }
        public string DateOfInsepections { get; set; } = string.Empty;
        public string DetailsOFDiscrepancy { get; set; } = string.Empty;
        public int PendingYearId { get; set; }
        public string PendingAmount { get; set; } = string.Empty;
        public int NoOfCases { get; set; }
        public string RemarkOfCases { get; set; } = string.Empty;
        public string RemarksBasedOnApplication { get; set; } = string.Empty;
        public string InsepectionsJson { get; set; } = string.Empty;
        public string PendingDuesJson { get; set; } = string.Empty;
        public string CheckBoxJson { get; set; } = string.Empty;
        public List<SelectListItem> lstYears { get; set; } = new List<SelectListItem>();
    }
    public class RequestModel
    {
        public int RequestId { get; set; }
    }
}
