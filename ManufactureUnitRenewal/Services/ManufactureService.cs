using DocumentFormat.OpenXml.Drawing.Charts;
using IEMS_FrontApplications.Models.ExciseMaster;
using IEMS_WEB.Areas.Manufacturer.Models;
using IEMS_WEB.Areas.ManufactureUnitRenewal.Interface;
using IEMS_WEB.Areas.ManufactureUnitRenewal.Model.Request;
using IEMS_WEB.Areas.Wallet.Models.Response;
using IEMS_WEB.Comman;
using IEMS_WEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace IEMS_WEB.Areas.ManufactureUnitRenewal.Services
{
    public class ManufactureService : IManufacturUnitRenewal
    {
        public async Task<ListManufacturingUnitRenewalSaveModel> ddlManufactureUnit(BaseModel basemodel)
        {
            ListManufacturingUnitRenewalSaveModel model = new ListManufacturingUnitRenewalSaveModel();
            //string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {

                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(basemodel);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/DdlManufactureUnit", json, basemodel.Token);


                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        model = Newtonsoft.Json.JsonConvert.DeserializeObject<ListManufacturingUnitRenewalSaveModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return model;
        }




        public async Task<BaseModel> PostManufactureUnit(ListManufacturingUnitRenewalSaveModel model)
        {
            BaseModel objmode = new BaseModel();
            // string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    json = json.Replace("null", "\"\"");
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/PostManufactureUnit", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objmode = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return objmode;
        }

        public async Task<List<SelectListItem>> GetDDLManufactureOrLicense(UnitModel basemodel)
        {
            List<SelectListItem> model = new List<SelectListItem>();
            //string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(basemodel);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/GetDDLManufactureOrLicense", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SelectListItem>>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return model;
        }

        public async Task<DeoDetailModel> GetDDLManufactureOrLicenseDeoDetail(BaseModel basemodel)
        {
            DeoDetailModel model = new DeoDetailModel();
            //string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(basemodel);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/GetDDLManufactureOrLicenseDeoDetail", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        model = Newtonsoft.Json.JsonConvert.DeserializeObject<DeoDetailModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return model;
        }
        public async Task<ListManufacturingUnitRenewalSaveModel> GetAllManufactureDetails(BaseModel model)
        {
            ListManufacturingUnitRenewalSaveModel objdetails = new ListManufacturingUnitRenewalSaveModel();
            //string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/GetAllManufactrueDetails", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objdetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ListManufacturingUnitRenewalSaveModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return objdetails;
        }



        public async Task<List<SelectListItem>> GetCheckList(FormModel formCode)
        {
            List<SelectListItem> model = new List<SelectListItem>();
            //string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                   string json = Newtonsoft.Json.JsonConvert.SerializeObject(formCode);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/GetCheckList", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        model = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SelectListItem>>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return model;
        }

        public async Task<List<ManufacturingUnitRenewalRequestParameter>> GetManufacturingUnitRenewalRequests(int status,int request)
        {
            List<ManufacturingUnitRenewalRequestParameter> lstManufacturingRenewals = new List<ManufacturingUnitRenewalRequestParameter>();
            using (var client = new HttpClient())
            {
                try
                {
                    string url = URLPORTServices.GetURL(URLPORT.Login);
                    #region Call API
                    var response = await HttpClientHelper.GetAPI(url + "Manufacture/GetManufacturingUnitRenewalRequests?status=" + status + "&RequestId="+request, string.Empty );
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        lstManufacturingRenewals = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ManufacturingUnitRenewalRequestParameter>>(data);

                    }
                }
                catch (Exception ex)
                {

                }
                return lstManufacturingRenewals;
            }
        }

        public async Task<ListManufacturingUnitRenewalSaveModel> GetAllManufactureDetailsByRequestId(UnitRenewalViewRequestModel model)
        {
            ListManufacturingUnitRenewalSaveModel objdetails = new ListManufacturingUnitRenewalSaveModel();
            //string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/GETUNITRENEWALDETAILS", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objdetails = Newtonsoft.Json.JsonConvert.DeserializeObject<ListManufacturingUnitRenewalSaveModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return objdetails;
        }


        public async Task<BaseModel> PostManufactureUnitRenewalAEO(AeoRemarkRenewal model)
        {
            BaseModel objmode = new BaseModel();
            // string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    json = json.Replace("null", "\"\"");
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/PostManufactureRenewalAEOUnit", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objmode = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return objmode;
        }

        public async Task<BaseModel> PostManufactureUnitRenewalDEO(DeoRemarkRenewal model)
        {
            BaseModel objmode = new BaseModel();
            // string url = URLPORTServices.GetURL(URLPORT.ManufactureUnitRenewal);
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    json = json.Replace("null", "\"\"");
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/PostManufactureRenewalDEOUnit", json, "");

                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objmode = Newtonsoft.Json.JsonConvert.DeserializeObject<BaseModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return objmode;
        }

        public async Task<ListManufacturingUnitRenewalSaveModel> GETUNITRENEWALDETAILS(UnitRenewalViewRequestModel reqModel)

        {
            ListManufacturingUnitRenewalSaveModel objnew = new ListManufacturingUnitRenewalSaveModel();
            using (var client = new HttpClient())
            {
                try
                {
                    #region Call API
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(reqModel);
                    string url = URLPORTServices.GetURL(URLPORT.Login);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/GETUNITRENEWALDETAILS", json, string.Empty);
                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        objnew = Newtonsoft.Json.JsonConvert.DeserializeObject<ListManufacturingUnitRenewalSaveModel>(data);

                    }
                    else
                    {
                        //stateResponse.Status = 0;
                        //stateResponse.Message = "User ID InValid";
                    }
                }
                catch (Exception ex)
                {

                }
                return objnew;
            }
        }
        public async Task<List<int>> GetSelectedCheckBoxListByRequestId(RequestModel model)
        {
            List<int> selectedCheckBoxList = new List<int>();
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {

                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/GetSelectedCheckBoxList", json, string.Empty);


                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        selectedCheckBoxList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return selectedCheckBoxList;
        }
        public async Task<EGrassGRN_Verify_ResponseModel> GRNVerify(EGrassGRN_VerifyModel model)
        {
            EGrassGRN_Verify_ResponseModel verify = new EGrassGRN_Verify_ResponseModel();
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {

                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/ManufactureUnitGRNVerify", json, string.Empty);


                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        verify = Newtonsoft.Json.JsonConvert.DeserializeObject<EGrassGRN_Verify_ResponseModel>(data);
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return verify;
        }

        public async Task<string> GetUnitAddress(RequestModel model)
        {
            string str = string.Empty;
            string url = URLPORTServices.GetURL(URLPORT.Login);
            using (var client = new HttpClient())
            {
                try
                {

                    #region Call API 
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                    var response = await HttpClientHelper.POSTAPI(url + "Manufacture/ManufactureUnitAddress", json, string.Empty);


                    #endregion
                    if (response != null)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        str = data;
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {

                }

            }
            return str;
        }
    }
}
