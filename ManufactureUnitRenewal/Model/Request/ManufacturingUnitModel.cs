using IEMS_WEB.Areas.Manufacturer.Models;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace IEMS_WEB.Areas.ManufactureUnitRenewal.Model.Request
{
    public class ManufacturingUnitModel
    {
        public int MainUnitNameId { get; set; }
        public int UnitNameId { get; set; }
        public string UnitTypeName { get; set; } = "";
        public string AddressOfUnit { get; set; } = "";
        public List<SelectListItem> lstMainUnitName { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> lstUnitName { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> CheckBoxItems { get; set; } = new List<SelectListItem>();
        //public bool IsChecked { get; set; }
        public string MainUnitName { get; set; } = string.Empty;
    }
    public class LicenseeIssuredUnit
    {
        public int LicenseeTypeId { get; set; }
        public int LicenseeRuleId { get; set; }
        public int ProductGroupId { get; set; }
        public string FeeDeposite { get; set; } = "";
        public string EstablishmentOfPantDate { get; set; } = "";
        public string CommencementOfProductionDate { get; set; } = "";
        public int FinanceYear { get; set; }
        public List<SelectListItem> lstLicenseeType { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> lstLicenseeRule { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> lstFinanceYear { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> lstProductGroup { get; set; } = new List<SelectListItem>();

        public string UnitName { get; set; }
        public string FinanceYearName { get; set; } = string.Empty;
        public string LicenceRuleName { get; set; } = string.Empty;
        public int LicenseeType { get; set; }
        public string LicenceTypeName { get; set; } = string.Empty;
    }
    public class RenewalFeeDetails
    {
        public string GRN { get; set; } = "";
        public string GRNDate { get; set; } = "";
        public int FeeTypeId { get; set; }
        public int FeeDepositAmmount { get; set; }
        public string UploadChalan { get; set; } = "";
        public List<SelectListItem> lstFeeType { get; set; } = new List<SelectListItem>();

    }
    public class LateFeeDetails
    {
        public string LateFeeGRN { get; set; } = "";
        public string LateFeeGRNDate { get; set; } = "";
        public int LateFeeTypeId { get; set; }
        public string LateFeeUploadChalan { get; set; } = "";
        public int LateFeeDepositedAmmount { get; set; }
        public List<SelectListItem> lstLateFeeType { get; set; } = new List<SelectListItem>();
    }
    public class ProductionCapacity
    {
        public int ProductGroupId { get; set; }
        public bool ProductionType { get; set; }
        public string ProductionTypeName { get; set; } = "";
        public string ImflInBL { get; set; } = "";
        public string ClInBl { get; set; } = "";
        public string ImflAndClInBl { get; set; } = "";
        public string ImflAndClInCases { get; set; } = "";
        public int SpiritInKlpd { get; set; }
        public int ProductionCapaictyInKlSprit { get; set; }
        public int ProductionCapacityIMFLInCases { get; set; }
        public int ProudctionCapacityCLInCases { get; set; }
        public List<lstprod> lstprod { get; set; } = new List<lstprod>();
    }
    public class lstprod
    {
        public int ProductGroupId { get; set; }
        public string ClInBl { get; set; }
        public string ImflAndClInCases { get; set; }
        public string ProductGroupName { get; set; }
        public string ProductTypeName { get; set; }

    }
    public class CTO
    {
        public bool ProductionType { get; set; }
        public int ProductGroupId { get; set; }
        public string ProductionTypeName { get; set; } = "";
        public string ImflInBL { get; set; } = "";
        public string ClInBl { get; set; } = "";
        public string ImflAndClInBl { get; set; } = string.Empty;
        public string ImflAndClInCases { get; set; } = "";
        public int SpiritInKlpd { get; set; }
        public int ProductionCapaictyInKlSprit { get; set; }
        public int ProductionCapacityIMFLInCases { get; set; }
        public int ProudctionCapacityCLInCases { get; set; }
        public string IssueDate { get; set; } = "";
        public string ValidFrom { get; set; } = "";
        public string ValidUpto { get; set; } = "";
        public List<lstcto> lstcto { get; set; } = new List<lstcto>();
    }
    public class lstcto
    {
        public int ProductGroupId { get; set; }
        public string ClInBl { get; set; }
        public string ImflAndClInCases { get; set; }
        public string ValidFrom { get; set; }
        public string ValidUpto { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductGroupName { get; set; }
        public string IssueDate { get; set; }

    }
    public class RajasthanPollutionControlBoard
    {
        public string ImflInBlPollutionControl { get; set; } = "";
        public string ClInBlPollutionControl { get; set; } = "";
        public string ImflInCasesPollutionControl { get; set; } = "";
        public string ClInCasesPollutionControl { get; set; } = "";
        public string ImflAndClInBlPollutionControl { get; set; } = "";
        public string ImflAndClInCasesPollutionControl { get; set; } = "";
        public string SpiritInKldPollutionControl { get; set; } = "";
        public string CtoCteIssureDatePollutionControl { get; set; } = "";
        public string ValidityFromCTO_CTE_ClPollutionControl { get; set; } = "";
        public string ValidityUptoCTO_CTE_CLPollutionControl { get; set; } = "";
        public string ValidityFromCTO_CTE_IMFLPollutionControl { get; set; } = "";
        public string VAlidityUptoCTO_CTE_IMFLPollutionControl { get; set; } = "";
        public string ValidityFromCTO_CTE_RS_ENAPollutionControl { get; set; } = "";
        public string ValidityUptoCTO_CTE_RS_ENAPollutionControl { get; set; } = "";
        public string DocumentPollutionControl { get; set; } = "";
        public string OtherDocumentPollutionControl { get; set; } = "";

    }
    public class StockDetailsInBl
    {
        public int ProductGroupId { get; set; }
        public int SpiritProduction { get; set; }
        public int SpiritImport { get; set; }
        public int SpiritExport { get; set; }
        public int Dispatched { get; set; }
        public int SpiritClosingStock { get; set; }
        public int IMFLProduction { get; set; }
        public int IMFLDispatch { get; set; }
        public int IMFLClosingStock { get; set; }
        public int CLProduction { get; set; }
        public int CLDispatch { get; set; }
        public int CLClosingStock { get; set; }
        public List<lststock> lststock { get; set; } = new List<lststock>();
    }
    public class lststock
    {
        public int ProductGroupId { get; set; }
        public string SpiritImport { get; set; }
        public string SpiritExport { get; set; }
        public string SpiritClosingStock { get; set; }
        public string SpiritProduction { get; set; }
        public string ProductCategory { get; set; }
        public string Dispatched { get; set; }
    }
    public class FinYearIncomeDetails
    {
        public int BottlingFee { get; set; }
        public int ExportFee { get; set; }
        public int ImportFee { get; set; }
        public int OtheFee { get; set; }
        public int Total { get; set; }
        
    }

    public class ApplyingFor
    {
        public bool Bravery { get; set; }
        public bool BearBootl { get; set; }
        public bool DesiMadira { get; set; }
        public bool BandhirBhandaraDesiMadira { get; set; }
        public bool Distilary { get; set; }
        public bool Heritaj { get; set; }
        public bool BandhitBhandaraHeritajMadira { get; set; }
        public bool BandhitBhandaraThokVikray { get; set; }
        public bool BharatNirmitVideshiMadira { get; set; }
        public bool BharatNirmitVideshiMadiraSub { get; set; }
        public bool BandhitBhandaraseThokVikriySubLiz { get; set; }
        public bool ThokVikrayBandhitBhandara { get; set; }
        public bool AudyogikPrayojan { get; set; }
        public bool Niyam68_3A_Rrajasthan { get; set; }
        public bool ParishodhitPrastav { get; set; }
        public bool Franchise { get; set; }

        public bool Niyam68_3_Rrajasthan { get; set; }
        public bool Winary { get; set; }
    }
    public class ListManufacturingUnitRenewalSaveModel : AppprovalModal
    {
        public ListManufacturingUnitRenewalSaveModel()
        {
            manufacturingUnitModel = new ManufacturingUnitModel();
            licenseeIssuredUnit = new LicenseeIssuredUnit();
            renewalFeeDetails = new RenewalFeeDetails();
            lateFeeDetails = new LateFeeDetails();
            productionCapacity = new ProductionCapacity();
            rajasthanPollutionControlBoard = new RajasthanPollutionControlBoard();
            stockDetailsInBl = new StockDetailsInBl();
            applyingFor = new ApplyingFor();
            AeoRemark = new AeoRemarkRenewal();
            DeoRemark = new DeoRemarkRenewal();
            checkboxitems = new Checkboxlistitems();
            finYearIncomeDetails = new FinYearIncomeDetails();
            FeeRequest = new List<FeeRequest>();
        }
        public ManufacturingUnitModel manufacturingUnitModel { get; set; }
        public LicenseeIssuredUnit licenseeIssuredUnit { get; set; }
        public RenewalFeeDetails renewalFeeDetails { get; set; }
        public LateFeeDetails lateFeeDetails { get; set; }
        public ProductionCapacity productionCapacity { get; set; }
        public CTO CTO { get; set; }
        public RajasthanPollutionControlBoard rajasthanPollutionControlBoard { get; set; }
        public StockDetailsInBl stockDetailsInBl { get; set; }
        public FinYearIncomeDetails finYearIncomeDetails { get; set; }
        //public List<ProposedProductionModel> ProductGroupModel { get; set; }
        public ApplyingFor applyingFor { get; set; }
     
        public string ProductCapacityJson { get; set; }
        public string CTOJson { get; set; }
        public string StockBLJson { get; set; }
        public string FeeJson { get; set; }
        public string RULEJson { get; set; }
        public AeoRemarkRenewal AeoRemark { get; set; }
        public DeoRemarkRenewal DeoRemark { get; set; }

        public int BLProductGroupId { get; set; }
        public Checkboxlistitems checkboxitems { get; set; }
        public List<FeeRequest> FeeRequest { get; set; } = new List<FeeRequest>();

        public int RequestId { get; set; } = 0;
        public string FormCode { get; set; } = string.Empty;
        public string FinYear { get; set; } = string.Empty;
        public List<int> SelectedCheckBoxList { get; set; } = new List<int>();
        public string PDF_Path { get; set; } = string.Empty;
        public string E_SIGN_PDF_Path { get; set; } = string.Empty;
    }
    public class SelectedCheckBox
    {
        public int CheckBoxID { get; set; }
    }

    public class FeeRequest
    {
        public string FeeType { get; set; } = string.Empty;
        public string GRN { get; set; } = string.Empty;
        public string GRNDate { get; set; }
        public string Amount { get; set; } = string.Empty;

    }
    public class Checkboxlistitems
    {
        public int SlNo { get; set; }
        public string Descriptipn { get; set; }
        public List<lstrule> lstrule { get; set; } = new List<lstrule>();
    }
    public class lstrule
    {
        public int SlNo { get; set; }
        public string Descriptipn { get; set; }
        public string RuleDescriptipn_Krutidev { get; set; }

    }
    public class ManufacturingUnitRenewalRequestParameter
    {
        public string FinYear { get; set; }
        public int MfgLicId { get; set; }
        public int MfgUnitRegReqId { get; set; }
        public string UnitNo { get; set; }
        public string UnitName { get; set; }
        public int RequestId { get; set; }

        public string Status { get; set; }
        public string LiceseeType { get; set; }

    }
    public class UnitRenewalViewRequestModel
    {
        public int REQUESTID { get; set; }
    }
    public class DeoDetailModel
    {
        public int UnitId { get; set; } = 0;
        public int DeoCode { get; set; } = 0;
        public int DeoId { set; get; } = 0;
    }
    public class UnitModel
    {
        public string RoleName { get; set; } = string.Empty;
        public string LicenseeCode { get; set; } = string.Empty;
        public string FinYear { set; get; } = string.Empty;
    }
    public class FormModel
    {
        public string FormCode { get; set; } = string.Empty;
    }
    public class EGrassGRN_VerifyModel
    {
        public int MerchantCode { get; set; }
        public string GRN_NO { get; set; }
        public int Amount { get; set; }
    }
    public class EGrassGRN_Verify_ResponseModel
    {
        public string Status { get; set; }
        public string GRN_BankDate { get; set; }
        public string EGrasEncryptedData { get; set; } = string.Empty;
        public int AUIN_NO { get; set; }

    }
    public class DEOPDF : AppprovalModal
    {

        public string PDF_Path { get; set; } = string.Empty;
        public string E_SIGN_PDF_Path { get; set; } = string.Empty;
        public Checkboxlistitems checkboxitems { get; set; }
        public List<SelectListItem> lstDeoname = new List<SelectListItem>();
        public string deoname { get; set; } = string.Empty;
        public string AddressOfUnit { get; set; } = "";
        public int RequestIdfile { get; set; } = 0;
        public string LicenceTypeName { get; set; } = string.Empty;
    }
    public class UnitRenewalRequestModel : BaseModel
    {
        public UnitRenewalRequestModel() { }
        public string reportType { get; set; }
        public string reqType { get; set; }
        public string generatedNo { get; set; }
        public string deoname { get; set; } = string.Empty;
        public int deoid { get; set; }

    }
    public class FeeChallanModel
    {
        public string slno { get; set; }
        public string FeeTypeId { get; set; }
        public string FeeType { get; set; }
        public string LateFeeGRN { get; set; }
        public string LateFeeGRNDate { get; set; }
        public string LateFeeUploadChalan { get; set; }
        public string LateFeeDepositedAmount { get; set; }
        public string ChallanName { get; set; }
    }
}