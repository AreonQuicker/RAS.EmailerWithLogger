using System.Collections.Generic;
using Newtonsoft.Json;

namespace RAS.EmailerWithLogger.Models
{
    public class GenericEmailItem : EmailItemBase
    {
        public GenericEmailItem()
        {
        }

        public GenericEmailItem(string subject) : base(subject)
        {
        }

        [JsonProperty("substitutions")]
        public Dictionary<string, string> Substitutions => new()
            {{"<%Title%>", Title}, {"<%Subject%>", Subject}, {"<%Body%>", Body}};

        public string Title { get; set; }
        public string Body { get; set; }
        public override string TemplateId => "d-6c3c42b7928148e79eec718aff4ac158";
    }
}