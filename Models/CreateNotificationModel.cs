namespace KIDS.API.Models
{
    public class ParamaterPushNotification
    {
        public string IdDevice { get; set; }
        public string ContentVi { get; set; }
        public string ContentEn { get; set; }
        public string data1 { get; set; }
        public string data2 { get; set; }
        public string TitleVi { get; set; }
        public string TitleEn { get; set; }
    }

    public class CreateNotificationModel
    {
        public string app_id { get; set; }
        public string[] include_player_ids { get; set; }
        public Data data { get; set; }
        public Headings headings { get; set; }
        public Contents contents { get; set; }
    }

    public class Data
    {
        public string id1 { get; set; }
        public string id2 { get; set; }
    }

    public class Headings
    {
        public string vi { get; set; }
        public string en { get; set; }
    }

    public class Contents
    {
        public string vi { get; set; }
        public string en { get; set; }
    }
}