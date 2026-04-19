namespace FilmolejBackend.Requests
{
    public class CompleteUploadRequest
    {
        public string UploadId { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }
    }
}
