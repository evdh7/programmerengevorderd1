using JobInterviewBL.Interfaces;
using JobInterviewDL;


namespace JobInterviewUtils
{
    public static class JobInterviewRepositoryFactory
    {
        public static IJobInterviewRepository GetJobinterviewRepository(string repoType)
        {
            switch (repoType)
            {
                case "memory": return new JobInterviewRepositoryMemory();
                default: return null;
            }

        }
    }
}