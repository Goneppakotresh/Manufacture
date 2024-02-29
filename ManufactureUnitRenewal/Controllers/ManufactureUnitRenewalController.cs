using IEMS_FrontApplications.Models.FileUpload;
using IEMS_WEB.Areas.ManufactureUnitRenewal.Interface;
using IEMS_WEB.Areas.ManufactureUnitRenewal.Model.Request;
using IEMS_WEB.Comman;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using IEMS_WEB.Models.PublicServices.Response;
using IEMS_FrontApplications.Interface.FileUpload;
using IEMS_WEB.Areas.Wallet.Models.Response;
using Newtonsoft.Json;
using IEMS_WEB.Areas.Bhang.Models;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using IEMS_WEB.Interface;
using IEMS_WEB.Areas.SpiritExportNOC.Interface;
namespace IEMS_WEB.Areas.ManufactureUnitRenewal.Controllers
{
    [Area("ManufactureUnitRenewal")]
    public class ManufactureUnitRenewalController : Controller
    {
        enum Licenceetypes
        {
            BOTTLING,
            BREWERY,
            DISTILLERY
        }
        SessionDetails objSession;
        string FormCode = "Manufacture_Unit_Renewal";
        public readonly IManufacturUnitRenewal _imanufacturUnit;
        private readonly IFileUpload _IFileUpload;

        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;
        private readonly IDropDownList _dropDownList;
        private void SetSessionValue()
        {
            objSession = HttpContext.Session.GetComplexData<SessionDetails>("SessionDetails");
        }
        public ManufactureUnitRenewalController(IManufacturUnitRenewal manufactureUnit, IFileUpload IFileUpload, IDropDownList idropDown, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _imanufacturUnit = manufactureUnit;
            _IFileUpload = IFileUpload;
            _hostingEnvironment = hostingEnvironment;
            _dropDownList = idropDown;
        }
        public async Task<IActionResult> ManufactureUnitRenewal()
        {
            ListManufacturingUnitRenewalSaveModel model = new ListManufacturingUnitRenewalSaveModel();
            SetSessionValue();

            BaseModel sessionmodel = new BaseModel();
            sessionmodel.LicenseeCode = objSession.LicenseeCode;

            if (HttpContext.Session.GetComplexData<ListManufacturingUnitRenewalSaveModel>("WorkflowData") != null)
            {
                model = HttpContext.Session.GetComplexData<ListManufacturingUnitRenewalSaveModel>("WorkflowData");
                TempData["FormCode"] = "AEO";
                TempData["FileRefId"] = 0;
                ListManufacturingUnitRenewalSaveModel objforAssigningWFData = new ListManufacturingUnitRenewalSaveModel();
                objforAssigningWFData = HttpContext.Session.GetComplexData<ListManufacturingUnitRenewalSaveModel>("WorkflowData");
                //WF Data Assign
                model.StageType = Convert.ToString(objforAssigningWFData.StageType == "" ? "I" : objforAssigningWFData.StageType);
                model.CurrStageId = objforAssigningWFData.CurrStageId;
                model.BoObjId = objforAssigningWFData.BoObjId;
                model.WorkFlowObjectId = objforAssigningWFData.WorkFlowObjectId;
                model.TransactionId = objforAssigningWFData.TransactionId;
                model.CurrRoleType = objforAssigningWFData.CurrRoleType;
                model.CurrRoleId = objforAssigningWFData.CurrRoleId;
                model.ApprovalStatus = objforAssigningWFData.ApprovalStatus;
                model.ActionId = 1;
                //var x = User.Identity.GetLocationCode;
                ViewBag.ddlManufacturUnitName = new List<SelectListItem>();
                ViewBag.ddlLicenseeUnitName = new List<SelectListItem>();
                ViewBag.ddlfinanceYear = new List<SelectListItem>();
                ViewBag.ddlRule = new List<SelectListItem>();
                ViewBag.ddlFeeType = new List<SelectListItem>();
                ViewBag.ddlProductGroup = new List<SelectListItem>();
                model.RoleName = User.Identity.GetRoleName();

                UnitRenewalViewRequestModel objreqedit = new UnitRenewalViewRequestModel();
                objreqedit.REQUESTID = model.TransactionId;

                ListManufacturingUnitRenewalSaveModel objGet = new ListManufacturingUnitRenewalSaveModel();

                objGet = await _imanufacturUnit.GETUNITRENEWALDETAILS(objreqedit);
                model.manufacturingUnitModel.AddressOfUnit = objGet.manufacturingUnitModel.AddressOfUnit;
                model.licenseeIssuredUnit.FinanceYearName = objGet.licenseeIssuredUnit.FinanceYearName;
                model.CTO.lstcto = objGet.CTO.lstcto;
                model.productionCapacity.lstprod = objGet.productionCapacity.lstprod;
                model.stockDetailsInBl.lststock = objGet.stockDetailsInBl.lststock;
                model.finYearIncomeDetails.BottlingFee = objGet.finYearIncomeDetails.BottlingFee;
                model.finYearIncomeDetails.ExportFee = objGet.finYearIncomeDetails.ExportFee;
                model.finYearIncomeDetails.ImportFee = objGet.finYearIncomeDetails.ImportFee;
                model.finYearIncomeDetails.OtheFee = objGet.finYearIncomeDetails.OtheFee;
                model.manufacturingUnitModel.MainUnitName = objGet.manufacturingUnitModel.MainUnitName;
                model.manufacturingUnitModel.UnitTypeName = objGet.manufacturingUnitModel.UnitTypeName;
                model.licenseeIssuredUnit.UnitName = objGet.licenseeIssuredUnit.UnitName;
                model.licenseeIssuredUnit.LicenceTypeName = objGet.licenseeIssuredUnit.LicenceTypeName;
                model.licenseeIssuredUnit.LicenceRuleName = objGet.licenseeIssuredUnit.LicenceRuleName;
                model.licenseeIssuredUnit.EstablishmentOfPantDate = objGet.licenseeIssuredUnit.EstablishmentOfPantDate;
                model.licenseeIssuredUnit.CommencementOfProductionDate = objGet.licenseeIssuredUnit.CommencementOfProductionDate;
                model.FeeRequest = objGet.FeeRequest;
                model.stockDetailsInBl = objGet.stockDetailsInBl;
                model.checkboxitems = objGet.checkboxitems;
                model.AeoRemark.lstInsection = objGet.AeoRemark.lstInsection;
                model.AeoRemark.lstFir = objGet.AeoRemark.lstFir;
                model.AeoRemark.lstPendingDues = objGet.AeoRemark.lstPendingDues;
                model.AeoRemark.lstAeoDetail = objGet.AeoRemark.lstAeoDetail;
                sessionmodel.licenseeCode = "0";
                var modelDDL = await _imanufacturUnit.ddlManufactureUnit(sessionmodel);
                model.AeoRemark.lstYears = modelDDL.AeoRemark.lstYears;
                model.DeoRemark.lstYears = modelDDL.DeoRemark.lstYears;
                RequestModel requestModel = new RequestModel();
                requestModel.RequestId = model.TransactionId;
                //if(model.RoleName == "DEO_APPROVER")
                //{
                //    model.SelectedCheckBoxList = await _imanufacturUnit.GetSelectedCheckBoxListByRequestId(requestModel);
                //}
                TempData["FormCode"] = "MANUFACTURE_UNIT_RENEWAL";
                TempData.Keep("FormCode");
                TempData["FileRefId"] = Convert.ToInt32(model.TransactionId);
                TempData.Keep("FileRefId");

                TempData["FormCode1"] = "AEO";
                TempData["FileRefId1"] = 0;
                TempData.Keep("FileRefId");
                if (model.RoleName == "DEO_APPROVER")
                {
                    TempData["FormCode1"] = "AEO";
                    TempData["FileRefId1"] = Convert.ToInt32(model.TransactionId);
                    TempData.Keep("FileRefId");
                }
            }
            else
            {
                model = await _imanufacturUnit.ddlManufactureUnit(sessionmodel);
                TempData["FormCode"] = "MANUFACTURE_UNIT_RENEWAL";
                TempData["FileRefId"] = 0;
                model.StageType = "S";
                model.CurrRoleType = "R";


                ViewBag.ddlManufacturUnitName = model.manufacturingUnitModel.lstMainUnitName;
                ViewBag.ddlLicenseeUnitName = model.licenseeIssuredUnit.lstLicenseeType;
                ViewBag.ddlfinanceYear = model.licenseeIssuredUnit.lstFinanceYear.Where(x=> x.Text == "select" || x.Text == "2024-2025").ToList();
                ViewBag.ddlRule = model.licenseeIssuredUnit.lstLicenseeRule;
                ViewBag.ddlFeeType = model.renewalFeeDetails.lstFeeType;
                ViewBag.ddlProductGroup = model.licenseeIssuredUnit.lstProductGroup;
            }
            // }
            FormModel formModel = new FormModel();
            formModel.FormCode = FormCode;
            model.manufacturingUnitModel.CheckBoxItems = await _imanufacturUnit.GetCheckList(formModel);
            foreach (var item in model.checkboxitems.lstrule)
            {
                model.manufacturingUnitModel.CheckBoxItems.Where(x => Convert.ToInt32(x.Value) == item.SlNo).ToList().ForEach(c => c.Selected = true);
            }


            var fIRCasesStatus = new List<SelectListItem>();
            fIRCasesStatus.Add(new SelectListItem() { Text = "Select", Value = "0" });
            fIRCasesStatus.Add(new SelectListItem() { Text = "PENDING", Value = "PENDING" });
            fIRCasesStatus.Add(new SelectListItem() { Text = "DISPOSED", Value = "DISPOSED" });

            ViewBag.FIRCasesStatus = fIRCasesStatus;

            var courtStay = new List<SelectListItem>();
            courtStay.Add(new SelectListItem() { Text = "Select", Value = "0" });
            courtStay.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
            courtStay.Add(new SelectListItem() { Text = "No", Value = "No" });

            ViewBag.CourtStay = courtStay;
            //ViewBag.ddlFee = model.lateFeeDetails.lstFee;
            model.manufacturingUnitModel.MainUnitNameId = Convert.ToInt32(objSession.LicenseeCode);
            model.licenseeIssuredUnit.LicenseeTypeId = Convert.ToInt32(objSession.LicenseeCode);
            model.FormCode = FormCode;
            //ViewBag.checklist = model.licenseeIssuredUnit.CheckBoxItems;
            ViewBag.test = new List<SelectListItem>();
            model.CurrRoleId = Convert.ToInt16(objSession.RoleId);
            model.LocationCode = Convert.ToInt16(objSession.LocationCode);
            model.LocationType = Convert.ToString(objSession.LocationType);
            // model.ListLicenseCode = await _IShopAllocation.GetBhangShopAllocationDropDown("LICENSECODE");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ManufactureUnitRenewal(ListManufacturingUnitRenewalSaveModel model2, int requestModel, string unitname, string rolename, string mainunitName, int FinYear)
        {
            var x = Request.Form.Files;
            List<IFormFile> feeChallanList = Request.Form.Files.Where(x => x.FileName.Contains("_LateFeeUploadChalanFile_")).ToList();
            //IFormFileCollection files = Request.Form.Files.Where(x => !x.FileName.Contains("lateFeeDetails.LateFeeUploadChalan") || !x.FileName.Contains("_LateFeeUploadChalanFile_"));
            //return View(model2);
            SetSessionValue();
            BaseModel model = new BaseModel();
            ListManufacturingUnitRenewalSaveModel objmodel = new ListManufacturingUnitRenewalSaveModel();
            ListManufacturingUnitRenewalSaveModel objmodel1 = new ListManufacturingUnitRenewalSaveModel();
            try
            {
                if (model2.StageType != "S")
                {
                    var objforAssigningWFData = HttpContext.Session.GetComplexData<ListManufacturingUnitRenewalSaveModel>("WorkflowData");
                    model2.manufacturingUnitModel.CheckBoxItems = new List<SelectListItem>();
                    model2.CTO = new CTO();
                    model2.LocationCode = objSession.LocationCode;
                    model2.LocationType = objSession.LocationType;
                    model2.UserID = objSession.UserID;
                    //model2.RoleId = objSession.RoleId;
                    model2.CurrRoleId = objSession.RoleId ?? 0;// == null ? 0 : objSession.RoleId;
                    model2.ActionId = model2.ActionId;
                    model2.StageType = Convert.ToString(model2.StageType == "" ? "I" : model2.StageType);
                    model2.CurrStageId = objforAssigningWFData.CurrStageId;
                    model2.BoObjId = objforAssigningWFData.BoObjId;
                    model2.WorkFlowObjectId = objforAssigningWFData.WorkFlowObjectId;
                    model2.TransactionId = objforAssigningWFData.TransactionId;
                    model2.CurrRoleType = objforAssigningWFData.CurrRoleType;
                    model2.CurrRoleId = objforAssigningWFData.CurrRoleId;
                    model2.ApprovalStatus = objforAssigningWFData.ApprovalStatus;
                    model2.FormCode = model2.FormCode;

                    model.RoleName = User.Identity.GetRoleName();
                    model.LocationCode = int.Parse(User.Identity.GetLocationCode());

                    if (model.RoleName == "DEO_APPROVER")
                    {
                        DEOPDF filemodel = new DEOPDF();




                        RequestModel requestModelnew = new RequestModel();
                        requestModelnew.RequestId = model2.TransactionId;
                        UnitRenewalViewRequestModel objreqedit = new UnitRenewalViewRequestModel();
                        objreqedit.REQUESTID = model2.TransactionId;


                        var renewalDetails = await _imanufacturUnit.GETUNITRENEWALDETAILS(objreqedit);
                        //model2.checkboxitems = model2.checkboxitems;

                        string reqid = objreqedit.REQUESTID.ToString();
                        //model2.checkboxitems = await _imanufacturUnit.GetSelectedCheckBoxListByRequestId(requestModelnew);
                        FileResult fileResult;
                        DropDownModel ObjDd = new DropDownModel();
                        ObjDd.DropDownType = "DEO_APPROVER";
                        ObjDd.ParentID = Convert.ToInt32(reqid);
                        filemodel.lstDeoname = await _dropDownList.GetDropDown(ObjDd);

                        UnitRenewalRequestModel reqdeo = new UnitRenewalRequestModel();
                        filemodel.lstDeoname = filemodel.lstDeoname.ToList().Skip(1).Take(2).ToList();
                        filemodel.deoname = filemodel.lstDeoname.ToList().FirstOrDefault().Text;
                        string deobane = filemodel.deoname;

                        filemodel.LicenceTypeName = renewalDetails.licenseeIssuredUnit.LicenceTypeName;
                        filemodel.checkboxitems = renewalDetails.checkboxitems;
                        filemodel.AddressOfUnit = renewalDetails.manufacturingUnitModel.AddressOfUnit;

                        fileResult = await DownlloadUnitRenewlPDF(filemodel, reqid, "", "Manufacture_Unit_Renewal", deobane);
                        //model2.PDF_Path = "C:\\EsignDoc\\"+ fileResult.FileDownloadName;
                        model2.PDF_Path = fileResult.FileDownloadName;
                        //model2.PDF_Path = TempData["Filepath"].ToString();

                        model2.E_SIGN_PDF_Path = "";
                    }


                    model = await _imanufacturUnit.PostManufactureUnit(model2);
                    UploadFile1(Request.Form.Files, "AEO", objforAssigningWFData.TransactionId);

                    //return View(objmodel);
                    return RedirectToAction("WFInbox", "Inbox", new { area = "" });
                }
                string stageid = string.Empty;
                stageid = model2.StageType = "S";
                if (requestModel == 0 || mainunitName == null || rolename == null || unitname == null)
                {
                    model2.lateFeeDetails.LateFeeUploadChalan = "adfuewjhnf";
                    model2.renewalFeeDetails.UploadChalan = "adfuewjhnf";
                    //var CheckBoxItems = new List<SelectedCheckBox>();

                    //foreach (var rec in model2.manufacturingUnitModel.CheckBoxItems)
                    //{
                    //    if (rec.Selected == true)
                    //    {

                    //        SelectListItem selectListItem = new SelectListItem();
                    //        selectListItem.Text = rec.Text;
                    //        selectListItem.Value = rec.Value;
                    //        CheckBoxItems.Add((new SelectedCheckBox() { CheckBoxID = Convert.ToInt32(selectListItem.Value) }));

                    //    }
                    //}
                    //model2.RULEJson = JsonConvert.SerializeObject(CheckBoxItems);
                    model2.manufacturingUnitModel.CheckBoxItems = new List<SelectListItem>();
                    //model2.LocationCode = 155;
                    model2.LocationType = "D";
                    model2.UserID = objSession.UserID;

                    //upload fee challan
                    List<FeeChallanModel> feeChallanModels = new List<FeeChallanModel>();
                    var feeDetails = JsonConvert.DeserializeObject<List<FeeChallanModel>>(model2.FeeJson);
                    foreach(var  file in Request.Form.Files)
                    {
                        if(file.Name.Contains("_LateFeeUploadChalanFile_"))
                        {
                            string FileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + file.FileName;
                            //string FileLabel = FileName + Path.GetExtension(Request.Form.Files[0].FileName);
                            string FilePath = "/FileUpload/manufacturingunit";
                            string UploadPath = Path.Combine(_hostingEnvironment.WebRootPath + FilePath, FileName);
                            using (FileStream fs = System.IO.File.Create(UploadPath))
                            {
                                file.CopyTo(fs);
                            }
                            var feeTypeId = file.Name.Split('_');
                            FeeChallanModel feeChallanModel = new FeeChallanModel();
                            feeChallanModel = feeDetails.Where(x => x.FeeTypeId == feeTypeId[0]).FirstOrDefault();
                            feeChallanModel.ChallanName = FileName;
                            feeChallanModels.Add(feeChallanModel);
                        }
                        
                    }


                    model2.FeeJson = JsonConvert.SerializeObject(feeChallanModels);







                    model = await _imanufacturUnit.PostManufactureUnit(model2);

                    if (model.ResponseModel == 2)
                    {
                        model.Message = "Application number is " + model.returnstatus + ". Application sent to OIC successfully!";
                    }
                    if (model.ResponseModel == 3)
                    {

                        model.Message = "Record already exists for the selected FY !";
                    }

                    if (model != null && model.ResponseModel == 1)
                    {
                        model.Message = "Saved Successfully";
                        TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Success, model.Message);
                    }
                    else if (model != null && model.ResponseModel == 0)
                    {
                        TempData["Alert"] = CommonMessageServices.ShowAlert(Alerts.Danger, model.Message);
                    }
                    model.LicenseeCode = objSession.LicenseeCode;
                    objmodel1 = await _imanufacturUnit.ddlManufactureUnit(model);
                    ViewBag.ddlManufacturUnitName = objmodel1.manufacturingUnitModel.lstMainUnitName;
                    //model2.licenseeIssuredUnit.FinanceYear = ViewBag.ddlManufacturUnitName;
                    ViewBag.ddlLicenseeUnitName = objmodel1.licenseeIssuredUnit.lstLicenseeType;
                    ViewBag.ddlfinanceYear = objmodel1.licenseeIssuredUnit.lstFinanceYear.Where(x => x.Text != "2023-2024").ToList();
                    ViewBag.ddlRule = objmodel1.licenseeIssuredUnit.lstLicenseeRule;
                    ViewBag.ddlFeeType = objmodel1.renewalFeeDetails.lstFeeType;
                    ViewBag.ddlProductGroup = objmodel1.licenseeIssuredUnit.lstProductGroup;

                    var fIRCasesStatus = new List<SelectListItem>();
                    fIRCasesStatus.Add(new SelectListItem() { Text = "Select", Value = "0" });
                    fIRCasesStatus.Add(new SelectListItem() { Text = "PENDING", Value = "PENDING" });
                    fIRCasesStatus.Add(new SelectListItem() { Text = "DISPOSED", Value = "DISPOSED" });

                    ViewBag.FIRCasesStatus = fIRCasesStatus;

                    var courtStay = new List<SelectListItem>();
                    courtStay.Add(new SelectListItem() { Text = "Select", Value = "0" });
                    courtStay.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
                    courtStay.Add(new SelectListItem() { Text = "No", Value = "No" });

                    ViewBag.CourtStay = courtStay;
                    //ViewBag.ddlFee = objmodel1.lateFeeDetails.lstFee;
                    string unitName = model2.manufacturingUnitModel.UnitTypeName;
                    objmodel.manufacturingUnitModel.UnitTypeName = unitName;
                    int unitNameId = model2.manufacturingUnitModel.UnitNameId;
                    objmodel.manufacturingUnitModel.MainUnitNameId = 4;
                    objmodel.manufacturingUnitModel.UnitNameId = unitNameId;
                    BaseModel basemodel1 = new BaseModel();
                    basemodel1.RoleName = "Manufacturing";
                    basemodel1.LicenseeCode = "4";
                    objmodel.ResponseModel = model.ResponseModel;
                    objmodel.Message = model.Message;
                    //if (objmodel.StageType == stageid)
                    //{
                    //model.Status = model.RequestId;

                    //foreach (var file in Request.Form.Files)
                    //{
                    //    if (!file.Name.Contains("_LateFeeUploadChalanFile_") && !file.Name.Contains("lateFeeDetails.LateFeeUploadChalan"))
                    //    {
                    //        UploadFile(Request.Form.Files, "MANUFACTURE_UNIT_RENEWAL", model.returnstatus);
                    //    }

                    //}

                    UploadFile(Request.Form.Files, "MANUFACTURE_UNIT_RENEWAL", model.returnstatus);
                    //}
                    UnitModel unitModel = new UnitModel();
                    basemodel1.RoleName = "Manufacturing";
                    basemodel1.LicenseeCode = "4";
                    ViewBag.test = await _imanufacturUnit.GetDDLManufactureOrLicense(unitModel);
                    return View(objmodel);
                    //return RedirectToAction("ManufactureUnitRenewal");

                }
                else
                {
                    model.LicenseeCode = Convert.ToString(requestModel);
                    objmodel = await _imanufacturUnit.GetAllManufactureDetails(model);
                    model.LicenseeCode = objSession.LicenseeCode;
                    objmodel1 = await _imanufacturUnit.ddlManufactureUnit(model);
                    ViewBag.ddlManufacturUnitName = objmodel1.manufacturingUnitModel.lstMainUnitName;
                    ViewBag.ddlLicenseeUnitName = objmodel1.licenseeIssuredUnit.lstLicenseeType;
                    ViewBag.ddlfinanceYear = objmodel1.licenseeIssuredUnit.lstFinanceYear.Where(x => x.Text != "2023-2024").ToList();
                    ViewBag.ddlRule = objmodel1.licenseeIssuredUnit.lstLicenseeRule;
                    ViewBag.ddlFeeType = objmodel1.renewalFeeDetails.lstFeeType;
                    ViewBag.ddlProductGroup = objmodel1.licenseeIssuredUnit.lstProductGroup;
                    //ViewBag.ddlFee = objmodel1.lateFeeDetails.lstFee; 
                    objmodel.manufacturingUnitModel.UnitTypeName = mainunitName;
                    objmodel.manufacturingUnitModel.MainUnitNameId = 4;
                    objmodel.manufacturingUnitModel.UnitNameId = requestModel;
                    objmodel.manufacturingUnitModel.UnitTypeName = unitname;
                    objmodel.manufacturingUnitModel.MainUnitNameId = Convert.ToInt32(objSession.LicenseeCode);
                    objmodel.licenseeIssuredUnit.LicenseeTypeId = Convert.ToInt32(objSession.LicenseeCode);
                    objmodel.licenseeIssuredUnit.FinanceYear = FinYear;

                    var fIRCasesStatus = new List<SelectListItem>();
                    fIRCasesStatus.Add(new SelectListItem() { Text = "Select", Value = "0" });
                    fIRCasesStatus.Add(new SelectListItem() { Text = "PENDING", Value = "PENDING" });
                    fIRCasesStatus.Add(new SelectListItem() { Text = "DISPOSED", Value = "DISPOSED" });

                    ViewBag.FIRCasesStatus = fIRCasesStatus;

                    var courtStay = new List<SelectListItem>();
                    courtStay.Add(new SelectListItem() { Text = "Select", Value = "0" });
                    courtStay.Add(new SelectListItem() { Text = "Yes", Value = "Yes" });
                    courtStay.Add(new SelectListItem() { Text = "No", Value = "No" });

                    ViewBag.CourtStay = courtStay;
                    UnitModel basemodel1 = new UnitModel();
                    basemodel1.RoleName = unitname;
                    basemodel1.LicenseeCode = rolename;
                    //basemodel1.FormCode = FormCode;
                    FormModel formModel = new FormModel();
                    formModel.FormCode = FormCode;
                    objmodel.manufacturingUnitModel.CheckBoxItems = await _imanufacturUnit.GetCheckList(formModel);
                    ViewBag.test = await _imanufacturUnit.GetDDLManufactureOrLicense(basemodel1);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return View(objmodel);
        }
        public async Task<IActionResult> GetUnitName(string unitname, string rolename, string finYear)
        {
            List<SelectListItem> model = new List<SelectListItem>();
            UnitModel basemodel = new UnitModel();
            basemodel.RoleName = unitname;
            basemodel.LicenseeCode = rolename;
            basemodel.FinYear = finYear;
            model = await _imanufacturUnit.GetDDLManufactureOrLicense(basemodel);
            return Json(new { data = model });
        }
        public async Task<IActionResult> GetRules(int LicenseTypeId)
        {
            BaseModel model = new BaseModel();
            model.LicenseeCode = Convert.ToString(LicenseTypeId);
            var modelDDL = await _imanufacturUnit.ddlManufactureUnit(model);
            return Json(modelDDL);
        }
        public async Task<IActionResult> GetDeoDetail(string unitname, string rolename, string selectedUnit)
        {
            List<SelectListItem> model1 = new List<SelectListItem>();
            BaseModel basemodel = new BaseModel();
            basemodel.RoleName = unitname;
            basemodel.LicenseeCode = rolename;
            basemodel.LocationCode = Convert.ToInt32(selectedUnit);
            DeoDetailModel model = await _imanufacturUnit.GetDDLManufactureOrLicenseDeoDetail(basemodel);
            RequestModel requestModel = new RequestModel();
            requestModel.RequestId = Convert.ToInt32(selectedUnit);
            string address = await _imanufacturUnit.GetUnitAddress(requestModel);
            model1.Add(new SelectListItem { Text = "DeoCode", Value = Convert.ToString(model.DeoCode) });
            model1.Add(new SelectListItem { Text = "Address", Value = address });
            return Json(model1);
        }
        private void UploadFile(IFormFileCollection files, string formCode, int status)
        {
            FileUploadRequest request = new FileUploadRequest();
            request.FormCode = formCode;
            request.TransactionId = status;
            var filedata = HttpContext.Session.GetComplexData<List<FileUploadRequestParameters>>("FileData");
            if (filedata != null)
            {

                foreach (IFormFile source in files)
                {
                    //using (var target = new MemoryStream())
                    //{
                    //    source.CopyToAsync(target);
                    //    target.ToArray();
                    //    source.CopyToAsync(target);
                    //    var filecontent = target.ToArray();
                    //    request.lstFiles.Add(new FileUploadRequestParameters { FileName = source.FileName, ValueField = filedata.Where(y => y.FileName == source.FileName).Select(x => x.ValueField).FirstOrDefault(), FileId = Convert.ToInt16(filedata.Where(y => y.FileName == source.FileName).Select(x => x.FileId).FirstOrDefault()), base64file = Convert.ToBase64String(filecontent) });
                    //}
                    if (source.Name.Contains("_LateFeeUploadChalanFile_") || source.Name.Contains("lateFeeDetails.LateFeeUploadChalan"))
                    {
                        
                    }
                    else
                    {
                        using (var binaryReader = new BinaryReader(source.OpenReadStream()))
                        {
                            // Read the contents of the IFormFile directly into a byte array
                            byte[] fileBytes = binaryReader.ReadBytes((int)source.Length);
                            // Convert the byte array to a Base64 string
                            string base64String = Convert.ToBase64String(fileBytes);
                            // Now 'base64String' contains the Base64 representation of the file
                            request.lstFiles.Add(new FileUploadRequestParameters { FileName = source.FileName, ValueField = filedata.Where(y => y.FileName == source.FileName).Select(x => x.ValueField).FirstOrDefault(), FileId = Convert.ToInt16(filedata.Where(y => y.FileName == source.FileName).Select(x => x.FileId).FirstOrDefault()), base64file = base64String });
                        }
                    }
                        
                }

                _IFileUpload.UploadFiles(request);
            }
            HttpContext.Session.Remove("FileData");
        }

        private void UploadFile1(IFormFileCollection files, string formCode, int status)
        {
            FileUploadRequest request = new FileUploadRequest();
            request.FormCode = formCode;
            request.TransactionId = status;
            var filedata = HttpContext.Session.GetComplexData<List<FileUploadRequestParameters>>("FileData1");
            if (filedata != null)
            {

                foreach (IFormFile source in files)
                {
                    //using (var target = new MemoryStream())
                    //{
                    //    source.CopyToAsync(target);
                    //    target.ToArray();
                    //    source.CopyToAsync(target);
                    //    var filecontent = target.ToArray();
                    //    request.lstFiles.Add(new FileUploadRequestParameters { FileName = source.FileName, ValueField = filedata.Where(y => y.FileName == source.FileName).Select(x => x.ValueField).FirstOrDefault(), FileId = Convert.ToInt16(filedata.Where(y => y.FileName == source.FileName).Select(x => x.FileId).FirstOrDefault()), base64file = Convert.ToBase64String(filecontent) });
                    //}
                    if (source.Name.Contains("_LateFeeUploadChalanFile_") || source.Name.Contains("lateFeeDetails.LateFeeUploadChalan"))
                    {

                    }
                    else
                    {
                        using (var binaryReader = new BinaryReader(source.OpenReadStream()))
                        {
                            // Read the contents of the IFormFile directly into a byte array
                            byte[] fileBytes = binaryReader.ReadBytes((int)source.Length);
                            // Convert the byte array to a Base64 string
                            string base64String = Convert.ToBase64String(fileBytes);
                            // Now 'base64String' contains the Base64 representation of the file
                            request.lstFiles.Add(new FileUploadRequestParameters { FileName = source.FileName, ValueField = filedata.Where(y => y.FileName == source.FileName).Select(x => x.ValueField).FirstOrDefault(), FileId = Convert.ToInt16(filedata.Where(y => y.FileName == source.FileName).Select(x => x.FileId).FirstOrDefault()), base64file = base64String });
                        }
                    }

                }

                _IFileUpload.UploadFiles(request);
            }
            HttpContext.Session.Remove("FileData1");
        }

        public ActionResult WorkFlowActionData(int WFObjectId, int RecordId, int Status, int WFXmlId, int MonthId, int StageId)
        {
            ListManufacturingUnitRenewalSaveModel objApprovalData = new ListManufacturingUnitRenewalSaveModel();
            try
            {
                SetSessionValue();
                objApprovalData.WorkFlowObjectId = WFObjectId;
                objApprovalData.TransactionId = RecordId;
                objApprovalData.ApprovalStatus = Status;
                objApprovalData.ApprovedBy = objSession.UserID;
                objApprovalData.CurrRoleId = Convert.ToInt32(objSession.RoleId);
                objApprovalData.CurrStageId = StageId;
                objApprovalData.CurrRoleType = "R";
                objApprovalData.CTO = new CTO();
                objApprovalData.productionCapacity = new ProductionCapacity();
                objApprovalData.manufacturingUnitModel = new ManufacturingUnitModel();
                objApprovalData.licenseeIssuredUnit = new LicenseeIssuredUnit();
                objApprovalData.renewalFeeDetails = new RenewalFeeDetails();
                objApprovalData.lateFeeDetails = new LateFeeDetails();
                objApprovalData.rajasthanPollutionControlBoard = new RajasthanPollutionControlBoard();
                objApprovalData.stockDetailsInBl = new StockDetailsInBl();
                objApprovalData.applyingFor = new ApplyingFor();
                objApprovalData.AeoRemark = new AeoRemarkRenewal();
                objApprovalData.DeoRemark = new DeoRemarkRenewal();
                objApprovalData.checkboxitems = new Checkboxlistitems();
                //objApprovalData.WfXmlId = WFXmlId;
                // TempData["WorkflowData"] = objApprovalData;
                HttpContext.Session.SetComplexData("WorkflowData", objApprovalData);
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("ManufactureUnitRenewal");
            //return RedirectToAction("UnitRenewalView");
        }

        public async Task<ActionResult> ViewDetails(int pkid, string actionName)
        {
            TempData["RenewalRequestId"] = pkid;
            TempData["Action"] = actionName;
            return Json("");
        }
        public async Task<ActionResult> ManufactureUnitRenewalView()
        {
            // TempData.Remove("ManufacturingRenewalWFData");
            //TempData.Remove("RenewalRequestId");
            //HttpContext.Session.Remove("ManufacturingRenewalWFData");

            return View();
        }
        public async Task<ActionResult> GetManufacturingRenewalRequests(int status = 0, int request = 0)
        {
            SetSessionValue();
            request = Convert.ToInt32(objSession.LicenseeCode);
            List<ManufacturingUnitRenewalRequestParameter> lstRenewalRequest = new List<ManufacturingUnitRenewalRequestParameter>();
            lstRenewalRequest = await _imanufacturUnit.GetManufacturingUnitRenewalRequests(status, request);
            return Json(new { data = lstRenewalRequest });
        }
        [HttpPost]
        public async Task<ActionResult> AEODetailSave(AeoRemarkRenewal model)
        {
            SetSessionValue();
            var deo = await _imanufacturUnit.PostManufactureUnitRenewalAEO(model);
            return Json(deo);
        }
        [HttpPost]
        public async Task<ActionResult> DEODetailSave(DeoRemarkRenewal model)
        {
            SetSessionValue();
            var deo = await _imanufacturUnit.PostManufactureUnitRenewalDEO(model);
            return Json(deo);
        }
        [HttpPost]
        public async Task<ActionResult> GRNVerify(EGrassGRN_VerifyModel model)
        {
            SetSessionValue();
            var deo = await _imanufacturUnit.GRNVerify(model);
            return Json(deo);
        }

        public async Task<ActionResult> UnitRenewalView()
        {
            UnitRenewalViewRequestModel objreqedit = new UnitRenewalViewRequestModel();
            objreqedit.REQUESTID = Convert.ToInt32(TempData["RenewalRequestId"]);

            //if (objreqedit.REQUESTID > 0)
            //{

            //}
            //else
            //{
            //    //ListManufacturingUnitRenewalSaveModel objApprovalData = HttpContext.Session.GetComplexData<ListManufacturingUnitRenewalSaveModel>("WorkflowData");
            //    //objreqedit.REQUESTID = objApprovalData.TransactionId;
            //}
            //ManufacturingRenewalViewResponseView objGet = new ManufacturingRenewalViewResponseView();
            //ManufacturingRenewalViewRequest model = new ManufacturingRenewalViewRequest();
            ListManufacturingUnitRenewalSaveModel objGet = new ListManufacturingUnitRenewalSaveModel();
            try
            {

                // objCo.Token = HttpContext.Session.GetString("token");
                //int a = 502;
                //objreqedit = a;
                objGet = await _imanufacturUnit.GETUNITRENEWALDETAILS(objreqedit);
                objGet.manufacturingUnitModel.AddressOfUnit = objGet.manufacturingUnitModel.AddressOfUnit;
                objGet.licenseeIssuredUnit.FinanceYearName = objGet.licenseeIssuredUnit.FinanceYearName;
                objGet.CTO.lstcto = objGet.CTO.lstcto;
                objGet.productionCapacity.lstprod = objGet.productionCapacity.lstprod;
                objGet.stockDetailsInBl.lststock = objGet.stockDetailsInBl.lststock;
                objGet.finYearIncomeDetails.BottlingFee = objGet.finYearIncomeDetails.BottlingFee;
                objGet.finYearIncomeDetails.ExportFee = objGet.finYearIncomeDetails.ExportFee;
                objGet.finYearIncomeDetails.ImportFee = objGet.finYearIncomeDetails.ImportFee;
                objGet.finYearIncomeDetails.OtheFee = objGet.finYearIncomeDetails.OtheFee;


                //model.FeeDeposite = objGet.List.FirstOrDefault().FeeDeposite;
                //model.MIN_YEARS = objGet.List.FirstOrDefault().MIN_YEARS;
                //model.TYPE = objGet.List.FirstOrDefault().TYPE;
                //model.EFFECTIVE_FROM = objGet.List.FirstOrDefault().EFFECTIVE_FROM;
                //model.EFFECTIVE_TO = objGet.List.FirstOrDefault().EFFECTIVE_TO;
                //model.LEVY_NAME = objGet.List.FirstOrDefault().LEVY_NAME;
                //model.PRODUCT_GROUP_NAME = objGet.List.FirstOrDefault().PRODUCT_GROUP_NAME;
                //if (model.TYPE == 1)
                //{
                // model.Type_NAME = "Brand";

                //}

                //else
                //{
                // model.Type_NAME = "Label";

                //}



                return View(objGet);
            }
            catch (Exception)
            {
                return View(objGet);
            }
        }

        public async Task<FileResult> DownlloadUnitRenewlPDF(DEOPDF model, string RequestId = "", string PermitNo = "", string Type = "Manufacture_Unit_Renewal", string deo = "")
        {
            //ListManufacturingUnitRenewalSaveModel model = new ListManufacturingUnitRenewalSaveModel();
            model.RequestIdfile = Convert.ToInt32(RequestId);
            //RequestId = "548";
            byte[] filebytes;
            if (!string.IsNullOrEmpty(RequestId)/* && !string.IsNullOrEmpty(PermitNo)*/)
            {
                UnitRenewalRequestModel req = new UnitRenewalRequestModel();
                req.generatedNo = PermitNo;
                req.reportType = "Unit_Renewal";
                req.reqType = Type;
                req.refId = User.Identity.GetUserRefId();
                req.UserID = User.Identity.GetId();
                req.licenseeCode = User.Identity.GetLicenseeCode();
                req.LicenseeType = User.Identity.GetLicenseeType();
                req.Token = User.Identity.GetToken();
                //model = await _fl_Six.GetUnitRenewalReports(req);
                model.deoname = deo;
                string Logopath = _hostingEnvironment.WebRootPath + DepartmentLogo.GetDeptLogo("excise");
                // Licenceetypes ltype = new Licenceetypes();
                //string licencetype = string.Empty;
                //licencetype = model.licenseeIssuredUnit.LicenceTypeName;
                if (model.LicenceTypeName == Licenceetypes.DISTILLERY.ToString())
                {
                    filebytes = UnitRenewal(model, Logopath);

                }

                else if (model.LicenceTypeName == Licenceetypes.BREWERY.ToString())
                {
                    filebytes = UnitRenewal(model, Logopath);

                }

                else if (model.LicenceTypeName == Licenceetypes.BOTTLING.ToString())
                {
                    filebytes = UnitRenewal(model, Logopath);

                }
                else
                {
                    filebytes = new byte[0];
                }
            }
            else
            {
                filebytes = new byte[0];
            }

           // var pdfpathfile = Path.Combine("C:\\EsignDoc", "Unit_Renewal" + RequestId + ".pdf");
            //// var pdfpath = Path.Combine("Unit_Renewal" + RequestId + ".pdf");
            // System.IO.File.WriteAllBytes(pdfpath, filebytes);
            // TempData["Filepath"] = pdfpath;
            // TempData.Keep("Filepath");




            var webRootPath = _hostingEnvironment.WebRootPath;

            var pdfpath = Path.Combine(webRootPath + "/FileUpload/manufacturingunit/" + "Unit_Renewal" + RequestId + ".pdf");
            //var pdfpath = Path.Combine("Unit_Renewal" + RequestId + ".pdf");
            System.IO.File.WriteAllBytes(pdfpath, filebytes);
            TempData["Filepath"] = pdfpath;
            TempData.Keep("Filepath");

            //string uploadsDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "FileUpload");
            // Directory.CreateDirectory(uploadsDirectory);

            // string filePath = Path.Combine(uploadsDirectory, pdfpath);
            //using (var fileStream = new FileStream(pdfpath, FileMode.Create))
            //{
            //    //await pdfpath.CopyToAsync(fileStream);
            //    pdfpath.CopyTo(fileStream)

            // }
            //load file data however you please
            return File(filebytes, "application/pdf", "Unit_Renewal" + RequestId + ".pdf");


        }

        public byte[] UnitRenewal(DEOPDF model, string logoPath)
        {
            byte[] fileContents = null;
            EncodingProvider ppp = CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
            string fontpath = Path.Combine(_hostingEnvironment.WebRootPath, "fonts", "Kruti Dev 010 Regular.ttf");
            BaseFont customfont = BaseFont.CreateFont(fontpath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(customfont, 15);
            iTextSharp.text.Font font1 = new iTextSharp.text.Font(customfont, 18, Font.BOLD);

            using (MemoryStream memoryStream = new MemoryStream())
            {

                using (Document document = new Document(new Rectangle(594, 842), 0f, 0f, 0f, 0f))
                {
                    PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                    document.Open();

                    Font blackFont = FontFactory.GetFont(FontFactory.HELVETICA, 12, Font.NORMAL, BaseColor.BLACK);
                    Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, BaseColor.BLACK);
                    Font blueBoldFont = FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD, BaseColor.BLUE);

                    PdfPTable leftTable1 = new PdfPTable(1);
                    PdfPCell leftCell1 = new PdfPCell(new Phrase("jktLFkku ljdkj", font1));
                    PdfPCell leftCell2 = new PdfPCell(new Phrase("dk;kZy; vkcdkjh vk;qä] jktLFkku]", font1));
                    PdfPCell leftCell3 = new PdfPCell(new Phrase("vkcdkjh Hkou] 2&xqekfu;kokyk] iapoVh] mn;iqj&313001", font));
                    leftCell1.Border = PdfPCell.NO_BORDER;
                    leftCell2.Border = PdfPCell.NO_BORDER;
                    leftCell3.Border = PdfPCell.NO_BORDER;
                    leftCell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    leftCell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    leftCell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    leftCell1.PaddingTop = 5f;
                    leftTable1.AddCell(leftCell1);
                    leftTable1.AddCell(leftCell2);
                    leftTable1.AddCell(leftCell3);

                    PdfPTable table1 = new PdfPTable(4);
                    Phrase phare1 = new Phrase("dekad ", font);
                    Phrase phare2 = new Phrase(model.RequestIdfile.ToString(), blackFont);
                    phare1.Add(phare2);
                    PdfPCell table1Cell1 = new PdfPCell(phare1);
                    table1Cell1.Colspan = 3;
                    table1Cell1.Border = PdfPCell.NO_BORDER;
                    //var dateAndTime = DateTime.Now;
                    //string date = dateAndTime.Date.ToString();

                    DateTime currentDateTime = DateTime.Now;
                    DateTime currentDate = currentDateTime.Date;
                    string date = currentDateTime.ToString("dd-MM-yy");

                    Phrase phare3 = new Phrase("fnukad% ", font);
                    Phrase phare4 = new Phrase(date, blackFont);
                    phare3.Add(phare4);
                    PdfPCell table1Cell2 = new PdfPCell(phare3);
                    table1Cell2.Border = PdfPCell.NO_BORDER;



                    PdfPCell table1Cell3 = new PdfPCell(new Phrase("ftyk vkcdkjh vfèkdkjh]", font));
                    table1Cell3.Colspan = 4;
                    table1Cell3.Border = PdfPCell.NO_BORDER;



                    PdfPTable table2 = new PdfPTable(1);
                    PdfPCell table2Cell1 = new PdfPCell(new Phrase(model.deoname, blackFont));
                    table2Cell1.Border = PdfPCell.NO_BORDER;

                    Phrase table2Cell1Pharse1 = new Phrase("fo\"k;%- eSllZ ", font);
                    Phrase table2Cell1Pharse2 = new Phrase(model.AddressOfUnit, blackFont);
                    Phrase table2Cell1Pharse5 = new Phrase("dks çnÙk vuqKki=ksa dks o\"kZ 2023&24 ds fy;s uohuhdj.k djus ckcrA", font);

                    Phrase table2Cell1Pharse6 = new Phrase("fo\"k;kUrxZr çklafxd i= ds Øe esa ys[k gS fd vkids çLrkokuqlkj eSll", font);
                    Phrase table2Cell1Pharse7 = new Phrase(model.AddressOfUnit, blackFont);
                    Phrase table2Cell1Pharse8 = new Phrase("dks çnÙk fuEukafdr vuqKki=ksa dk o\"kZ 2023&24 ds fy;s uohuhdj.k djus dh Loh—fr funsZ'kkuqlkj fuEukafdr 'krksZ ds vè;èkhu çnku dh tkrh gS %&", font);

                    table2Cell1Pharse1.Add(table2Cell1Pharse2);
                    table2Cell1Pharse1.Add(table2Cell1Pharse5);

                    table2Cell1Pharse6.Add(table2Cell1Pharse7);
                    table2Cell1Pharse6.Add(table2Cell1Pharse8);


                    PdfPCell table2Cell2 = new PdfPCell(table2Cell1Pharse1);
                    table2Cell2.Border = PdfPCell.NO_BORDER;
                    PdfPCell table2Cell21 = new PdfPCell(table2Cell1Pharse6);
                    table2Cell21.Border = PdfPCell.NO_BORDER;
                    table2Cell21.PaddingTop = 10;

                    PdfPTable table3 = new PdfPTable(6);

                    string slno = string.Empty;
                    for (int i = 0; i < model.checkboxitems.lstrule.Count; i++)
                    {

                        slno = (i + 1).ToString();
                        PdfPCell table3cell1 = new PdfPCell(new Phrase(slno, font1));
                        table3cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        table3cell1.PaddingBottom = 10;
                        PdfPCell table3cell2 = new PdfPCell(new Phrase(model.checkboxitems.lstrule[i].RuleDescriptipn_Krutidev, font1));
                        table3cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                        table3cell2.Colspan = 5;
                        table3cell2.PaddingBottom = 10;
                        table3.AddCell(table3cell1);
                        table3.AddCell(table3cell2);

                    }

                    //PdfPCell table3cell1 = new PdfPCell(new Phrase("d-la-", font1));
                    //table3cell1.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    //table3cell1.PaddingBottom = 10;
                    //PdfPCell table3cell2 = new PdfPCell(new Phrase("vuqKki= dk fooj.k", font1));
                    //table3cell2.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    //table3cell2.Colspan = 5;
                    //table3cell2.PaddingBottom = 10;

                    //PdfPCell table3cell3 = new PdfPCell(new Phrase("1.", blueBoldFont));
                    //table3cell3.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    //PdfPCell table3cell4 = new PdfPCell(new Phrase("Hkkjr fuÆer fons'kh efnjk cksVÇyx lc yht vuqKki=", font));
                    //table3cell4.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    //table3cell4.Colspan = 5;
                    //table3cell4.PaddingBottom = 10;

                    //PdfPCell table3cell5 = new PdfPCell(new Phrase("2.", blueBoldFont));
                    //table3cell5.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    //PdfPCell table3cell6 = new PdfPCell(new Phrase("cafèkr Hk.Mkxkj ls Hkk-fu-fo-e- Fkksd foØ; lc yht vuqKki=", font));
                    //table3cell6.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    //table3cell6.Colspan = 5;
                    //table3cell6.PaddingBottom = 10;


                    ///below things
                    PdfPCell table3cell9 = new PdfPCell(new Phrase("1.", blueBoldFont));
                    table3cell9.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table3cell9.Border = PdfPCell.NO_BORDER;
                    Phrase table3Phrase1 = new Phrase("jktLFkku çnw\"k.k fu;a=.k e.My] t;iqj ds vkns'k fnukad", font);
                    Phrase table3Phrase2 = new Phrase(date, blueBoldFont);
                    Phrase table3Phrase3 = new Phrase("ds }kjk tkjh ", font);
                    Phrase table3Phrase4 = new Phrase(" Consent to Operate ", blueBoldFont);
                    Phrase table3Phrase5 = new Phrase("ds vuqlkj bdkÃ }kjk fnukad", font);
                    Phrase table3Phrase6 = new Phrase(date, blueBoldFont);
                    Phrase table3Phrase7 = new Phrase(" ls ", font);
                    Phrase table3Phrase8 = new Phrase(date, blueBoldFont);
                    Phrase table3Phrase9 = new Phrase("rd Hkk-fu-fo-e- @ ns'kh efnjk mRiknu {kerk 18000 dSlst çfrfnu ls vfèkd dk mRiknu ugh fd;k tk ldsxkA", font);
                    table3Phrase1.Add(table3Phrase2);
                    table3Phrase1.Add(table3Phrase3);
                    table3Phrase1.Add(table3Phrase4);
                    table3Phrase1.Add(table3Phrase5);
                    table3Phrase1.Add(table3Phrase6);
                    table3Phrase1.Add(table3Phrase7);
                    table3Phrase1.Add(table3Phrase8);
                    table3Phrase1.Add(table3Phrase9);
                    PdfPCell table3cell10 = new PdfPCell(table3Phrase1);
                    table3cell10.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table3cell10.Colspan = 5;
                    table3cell10.Border = PdfPCell.NO_BORDER;

                    PdfPCell table3cell11 = new PdfPCell(new Phrase("2.", blueBoldFont));
                    table3cell11.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table3cell11.Border = PdfPCell.NO_BORDER;
                    PdfPCell table3cell12 = new PdfPCell(new Phrase("o\"kZ ds nkSjku bdkÃ dh efnjk mRiknu {kerk c<k;h tkrh gS] rks jktLFkku jkT; çnw\"k.k fu;a=.k e.My }kjk tkjh uohu ,u-vks-lh-@lhVhvks dh çfr ds lkFk l{ke vuqefr gsrq çLrko eq[;ky; dks çsf\"kr djuk gksxkA\r\n", font));
                    table3cell12.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table3cell12.Colspan = 5;
                    table3cell12.Border = PdfPCell.NO_BORDER;

                    PdfPCell table3cell13 = new PdfPCell(new Phrase("3.", blueBoldFont));
                    table3cell13.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table3cell13.Border = PdfPCell.NO_BORDER;
                    PdfPCell table3cell14 = new PdfPCell(new Phrase(";wfuV ds fo:} foxr o\"kksZ ,oa pkyq foÙkh; o\"kZ ds nkSjku fdlh çdkj dh foHkkxh; cdk;k ¼cdk;k ds fo:} ekuuh; U;k;ky; esa LVs çkIr çdj.kksa dks NksMdj½ gS] rks fu;ekuqlkj rRdky jktdks\"k esa tekjkt djk;h tkdj ikyuk çfrosnu çLrqr djuk gksxkA", font));
                    table3cell14.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table3cell14.Colspan = 5;
                    table3cell14.Border = PdfPCell.NO_BORDER;

                    PdfPCell table3cell15 = new PdfPCell(new Phrase("4.", blueBoldFont));
                    table3cell15.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table3cell15.Border = PdfPCell.NO_BORDER;
                    PdfPCell table3cell16 = new PdfPCell(new Phrase("bdkÃ }kjk ykÃlsUl 'krksZ ,oa foHkkxh; funsZ'kks dh ikyuk ugh fd;s tkus ij fu;ekuqlkj vfHk;ksx iath—r fd;k tk;sxkA", font));
                    table3cell16.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table3cell16.Colspan = 5;
                    table3cell16.Border = PdfPCell.NO_BORDER;


                    PdfPCell table3cell151 = new PdfPCell(new Phrase("5.", blueBoldFont));
                    table3cell151.HorizontalAlignment = PdfPCell.ALIGN_CENTER;
                    table3cell151.Border = PdfPCell.NO_BORDER;
                    PdfPCell table3cell161 = new PdfPCell(new Phrase("vkcdkjh uhfr 2022&23 ,oa 2023&24 ds fcUnq la[;k 11 ¼1½ ,oa ¼2½ ds rgr gksyksxzke ls ;qä D;wvkj dksM ds çkoèkkuksa dh fØ;kfUofr lqfuf'pr djsA", font));
                    table3cell161.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
                    table3cell161.Colspan = 5;
                    table3cell161.Border = PdfPCell.NO_BORDER;



                    PdfPTable table4 = new PdfPTable(1);
                    PdfPCell table4cell1 = new PdfPCell(new Phrase("vuqKki= ,oa lEcfUèkr vfHkys[k esa uohuhdj.k dh çfof\"B;ka vfuok;Z :i ls vafdr dh tkosA", font));
                    table4cell1.Border = PdfPCell.NO_BORDER;

                    PdfPTable table5 = new PdfPTable(1);
                    PdfPCell table5cell1 = new PdfPCell(new Phrase("vkcdkjh vk;qä]", font));
                    table5cell1.Border = PdfPCell.NO_BORDER;
                    table5cell1.PaddingTop = 80f;
                    table5cell1.PaddingLeft = 350f;

                    PdfPCell table5cell2 = new PdfPCell(new Phrase("jktLFkku] mn;iqj A", font));
                    table5cell2.Border = PdfPCell.NO_BORDER;
                    table5cell2.PaddingLeft = 350f;

                    PdfPCell table5cell3 = new PdfPCell(new Phrase("Digitally signed by b406376f39ee2e48\r\nDate: 2023.04.06 17:41.46+05:30\r\nReason: KUMAR PAL GAUTAM Location: Fxcise Commissioner\r\nUdaipu", blackFont));
                    table5cell3.Border = PdfPCell.NO_BORDER;
                    table5cell3.PaddingTop = 50f;
                    table5cell3.PaddingLeft = 250f;
                    //table5cell3.HorizontalAlignment = PdfPCell.ALIGN_RIGHT;


                    table1.AddCell(table1Cell1);
                    table1.AddCell(table1Cell2);
                    table1.AddCell(table1Cell3);

                    table2.AddCell(table2Cell1);
                    table2.AddCell(table2Cell2);
                    table2.AddCell(table2Cell21);

                    //table3.AddCell(table3cell1);
                    //table3.AddCell(table3cell2);
                    //table3.AddCell(table3cell3);
                    //table3.AddCell(table3cell4);
                    //table3.AddCell(table3cell5);
                    //table3.AddCell(table3cell6);

                    table3.AddCell(table3cell9);
                    table3.AddCell(table3cell10);
                    table3.AddCell(table3cell11);
                    table3.AddCell(table3cell12);
                    table3.AddCell(table3cell13);
                    table3.AddCell(table3cell14);
                    table3.AddCell(table3cell15);
                    table3.AddCell(table3cell16);
                    table3.AddCell(table3cell151);
                    table3.AddCell(table3cell161);
                    table4.AddCell(table4cell1);
                    table5.AddCell(table5cell1);
                    table5.AddCell(table5cell2);
                    table5.AddCell(table5cell3);

                    #region Added image Logo
                    //string imagePath = @"D:\\SugareCane02-01-2024\\FrontEnd_MainBranch27Dec2023\\FrontEnd_MainBranch27Dec2023\\FrontEnd_MainBranch\\IEMS_WEB\\IEMS_WEB\\wwwroot\\assets\\images\\QRCode.png";
                    //Image myImage = Image.GetInstance(imagePath);
                    // myImage.SetAbsolutePosition(400, 190);
                    #endregion

                    document.Add(leftTable1);
                    document.Add(table1);
                    document.Add(table2);
                    document.Add(table3);
                    document.Add(table4);
                    //document.Add(myImage);
                    document.Add(table5);
                    document.Close();
                    return fileContents = memoryStream.ToArray();
                }




            }

        }
    }
}