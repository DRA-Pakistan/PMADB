

namespace LightSwitchApplication.Poco
{
    public class FileUploadRequestParameters
    {
        public string FileName { get; set; }
        public byte[] BinaryData { get; set; }
        public string ReferencedEntitySet { get; set; }
        public string ReferenceId { get; set; }
        public int RecordId { get; set; }
    }
    
}