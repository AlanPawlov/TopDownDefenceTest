namespace Common.WebRequest
{
    public struct FileLoadingProgress
    {
        public int FileIndex;
        public float Progress;
        public string FileName;

        public FileLoadingProgress(int fileIndex)
        {
            FileIndex = fileIndex;
            Progress = 0f;
            FileName = string.Empty;
        }

        public void SetNameFile(string modelType)
        {
            FileName = modelType;
            FileIndex += 1;
            Progress = 0f;

        }
    }
}