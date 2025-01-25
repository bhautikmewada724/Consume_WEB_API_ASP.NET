using Newtonsoft.Json;

namespace ApiConsume.Models
{   
    public class ApiResultData<T> where T : class
    {
        public string Status { get; set; }
        public T Data { get; set; }
    }
    public class JsonOperation<T> where T : class
    {
        public async Task<ApiResultData<T>> jsonDeserialization(dynamic response)
        {
            ApiResultData<T> apiResultData = new ApiResultData<T>();
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result);
            var jsonData = JsonConvert.SerializeObject(jsonResponse.data);
            apiResultData.Data = JsonConvert.DeserializeObject<T>(jsonData);
            apiResultData.Status = jsonResponse["status"];
            return apiResultData;
        }

        public static string JsonSerialization(T model)
        {
            return JsonConvert.SerializeObject(model);
        }
    }
}
