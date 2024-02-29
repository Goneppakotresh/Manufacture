using DocumentFormat.OpenXml.Drawing.Charts;
using IEMS_WEB.Areas.ManufactureUnitRenewal.Model.Request;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IEMS_WEB.Areas.ManufactureUnitRenewal.Interface
{
    public interface IManufacturUnitRenewal
    {
        Task<ListManufacturingUnitRenewalSaveModel> ddlManufactureUnit(BaseModel model);
        Task<List<SelectListItem>> GetDDLManufactureOrLicense(UnitModel model);
        Task<DeoDetailModel> GetDDLManufactureOrLicenseDeoDetail(BaseModel model);
        Task<BaseModel> PostManufactureUnit(ListManufacturingUnitRenewalSaveModel model);
        Task<ListManufacturingUnitRenewalSaveModel> GetAllManufactureDetails(BaseModel model);

        Task<List<SelectListItem>> GetCheckList(FormModel formCode);
        Task<List<ManufacturingUnitRenewalRequestParameter>> GetManufacturingUnitRenewalRequests(int status, int request);
        Task<ListManufacturingUnitRenewalSaveModel> GetAllManufactureDetailsByRequestId(UnitRenewalViewRequestModel model);
        Task<BaseModel> PostManufactureUnitRenewalAEO(AeoRemarkRenewal model);
        Task<BaseModel> PostManufactureUnitRenewalDEO(DeoRemarkRenewal model);

        Task<ListManufacturingUnitRenewalSaveModel> GETUNITRENEWALDETAILS(UnitRenewalViewRequestModel reqModel);
        Task<List<int>> GetSelectedCheckBoxListByRequestId(RequestModel model);
        Task<EGrassGRN_Verify_ResponseModel> GRNVerify(EGrassGRN_VerifyModel model);
        Task<string> GetUnitAddress(RequestModel model);
    }
}
