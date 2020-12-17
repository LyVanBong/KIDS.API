using KIDS.API.Database;
using KIDS.API.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Linq;
using System.Web.Http;

namespace KIDS.API.Controllers
{
    [RoutePrefix("api/v1/PushNotifications")]
    public class PushNotificationController : ApiController
    {
        private H_KIDSEntities _entities;

        public PushNotificationController()
        {
            _entities = new H_KIDSEntities();
        }

        /// <summary>
        /// Gửi thông báo cho user
        /// </summary>
        /// <returns></returns>
        [Route("Send")]
        [HttpPost]
        public IHttpActionResult SendNotification([FromBody] ParamaterPushNotification pushNotification)
        {
            var client = new RestClient("https://onesignal.com/api/v1/notifications");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", " Basic NWU0ODZmMjctODMwOC00YzFlLWFkZDQtNTA3NDQ1ZDUxZmM5");
            request.AddHeader("Content-Type", "application/json");
            var message = new CreateNotificationModel
            {
                app_id = "4039833c-4862-44d5-b3a6-4045f71106b3",
                include_player_ids = new[] { pushNotification.IdDevice },
                headings = new Headings
                {
                    vi = "Thông báo mới",
                    en = "Notification"
                },
                contents = new Contents
                {
                    vi = pushNotification.ContentVi,
                    en = pushNotification.ContentEn
                },
                data = new Data
                {
                    id1 = pushNotification.data1,
                    id2 = pushNotification.data2
                }
            };
            var para = JsonConvert.SerializeObject(message);
            request.AddParameter("application/json", para, ParameterType.RequestBody);
            var response = client.Execute(request);
            if (response.IsSuccessful)
            {
                return Ok(new ResponseModel<string>
                {
                    Code = 1116,
                    Message = "Thanh cong",
                    Data = response.Content
                });
            }
            else
            {
                return Ok(new ResponseModel<string>
                {
                    Code = -1116,
                    Message = "Loi phat sinh",
                    Data = response.ErrorMessage
                });
            }
        }

        /// <summary>
        /// Cập lại id thiết bị để gửi thông báo
        /// </summary>
        /// <param name="pushNotificationModel"></param>
        /// <returns></returns>
        [Route("Update")]
        [HttpPost]
        public IHttpActionResult Update([FromBody] PushNotificationModel pushNotificationModel)
        {
            if (pushNotificationModel != null)
            {
                var data = _entities.sp_UpdateIdDevice(pushNotificationModel.IdSchool, pushNotificationModel.IdClass,
                    pushNotificationModel.IdUser, pushNotificationModel.IdDevice, pushNotificationModel.Note);
                if (data > 0)
                {
                    return Ok(new ResponseModel<int>
                    {
                        Code = 1115,
                        Message = "Thanh cong",
                        Data = data,
                    });
                }
                else
                {
                    return Ok(new ResponseModel<string>
                    {
                        Code = -1115,
                        Message = "Loi phat sinh",
                        Data = null,
                    });
                }
            }
            else
            {
                return Ok(new ResponseModel<string>
                {
                    Code = -1115,
                    Message = "Loi phat sinh",
                    Data = null,
                });
            }
        }

        /// <summary>
        /// Lấy Id thiết bị
        /// </summary>
        /// <param name="groupId">
        /// 0 tim kiem theo id user
        /// 1 tim kiem theo lop
        /// 2 tim kiem theo truong
        /// </param>
        /// <param name="id">
        /// id
        /// </param>
        /// <returns></returns>
        [Route("Select")]
        [HttpGet]
        public IHttpActionResult GetIdDeviceUser(int groupId, string id)
        {
            switch (groupId)
            {
                case 0:
                    var data = _entities.sp_GetIdDeviceUser(id);
                    if (data != null)
                    {
                        var userDevice = data.FirstOrDefault();
                        if (userDevice != null)
                        {
                            return Ok(new ResponseModel<string>
                            {
                                Code = 1111,
                                Message = "Lay du lieu thanh cong",
                                Data = userDevice.IDDevice,
                            });
                        }
                        else
                        {
                            return Ok(new ResponseModel<string>
                            {
                                Code = -1111,
                                Message = "Lay du lieu khong thanh cong",
                                Data = null,
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ResponseModel<string>
                        {
                            Code = -1111,
                            Message = "Lay du lieu khong thanh cong",
                            Data = null,
                        });
                    }
                case 1:
                    var dataClass = _entities.sp_GetIdDeviceClass(id);
                    if (dataClass != null)
                    {
                        var idDeviceClass = dataClass.ToList();
                        if (idDeviceClass.Any())
                        {
                            var idClass = ",";
                            idDeviceClass.ForEach((x) =>
                            {
                                idClass += $"{x},";
                            });
                            return Ok(new ResponseModel<string>
                            {
                                Code = 1112,
                                Message = "Thanh cong",
                                Data = idClass,
                            });
                        }
                        else
                        {
                            return Ok(new ResponseModel<string>
                            {
                                Code = -1112,
                                Message = "Lay du lieu khong thanh cong",
                                Data = null,
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ResponseModel<string>
                        {
                            Code = -1112,
                            Message = "Lay du lieu khong thanh cong",
                            Data = null,
                        });
                    }
                case 2:
                    var idSchool = _entities.sp_GetIdDeviceSchool(id);
                    if (idSchool != null)
                    {
                        var idDeviceSchool = idSchool.ToList();
                        if (idDeviceSchool.Any())
                        {
                            var idClass = ",";
                            idDeviceSchool.ForEach((x) =>
                            {
                                idClass += $"{x},";
                            });
                            return Ok(new ResponseModel<string>
                            {
                                Code = 1113,
                                Message = "Thanh cong",
                                Data = idClass,
                            });
                        }
                        else
                        {
                            return Ok(new ResponseModel<string>
                            {
                                Code = -1113,
                                Message = "Lay du lieu khong thanh cong",
                                Data = null,
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ResponseModel<string>
                        {
                            Code = -1113,
                            Message = "Lay du lieu khong thanh cong",
                            Data = null,
                        });
                    }
                default:
                    var idAllUser = _entities.sp_GetIdDeviceAllUser();
                    if (idAllUser != null)
                    {
                        var dataAllUser = idAllUser.ToList();
                        if (dataAllUser.Any())
                        {
                            var allUser = ",";
                            dataAllUser.ForEach(result =>
                            {
                                allUser += $"{result.IDDevice},";
                            });
                            return Ok(new ResponseModel<string>
                            {
                                Code = 1114,
                                Message = "Thanh cong",
                                Data = allUser,
                            });
                        }
                        else
                        {
                            return Ok(new ResponseModel<string>
                            {
                                Code = -1114,
                                Message = "Lay du lieu khong thanh cong",
                                Data = null,
                            });
                        }
                    }
                    else
                    {
                        return Ok(new ResponseModel<string>
                        {
                            Code = -1114,
                            Message = "Lay du lieu khong thanh cong",
                            Data = null,
                        });
                    }
            }
        }
    }
}