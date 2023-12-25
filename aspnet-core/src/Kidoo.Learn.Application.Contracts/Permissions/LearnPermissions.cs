namespace Kidoo.Learn.Permissions;

public static class LearnPermissions
{
    public const string GroupName = "Learn";

    public static class Students
    {
        public const string Default = GroupName + ".Students";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string ResetPassword = Default + ".ResetPassword";
        public const string Delete = Default + ".Delete";
        public const string Approve = Default + ".Approve";
    }
    public static class Question
    {
        public const string Default = GroupName + ".Questions";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }

    public static class Exam
    {
        public const string Default = GroupName + ".Exams";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
        public const string PublishExam = Default + ".PublishExam";
        public const string PublishResult = Default + ".PublishResult";
    }
    public static class ExamResult
    {
        public const string Default = GroupName + ".ExamResults";
        public const string Delete = Default + ".Delete";
        public const string Edit = Default + ".Edit";
        public const string Answers = Default + ".Answers";
        public const string ReSubmission = Default + ".ReSubmission";
        public const string ApproveAllExam = Default + ".ApproveAllExam";
        public const string ApproveAllAnswer = Default + ".ApproveAllAnswer";
    }
    public static class ExamAnswer
    {
        public const string Default = GroupName + ".ExamAnswers";
        public const string Approve = Default + ".Approve";
        public const string Edit = Default + ".Edit";
    }

    public static class StoryBook
    {
        public const string Default = GroupName + ".StoryBook";
        public const string Bangla = GroupName + ".StoryBook.Bangla";
        public const string English = GroupName + ".StoryBook.English";
    }
    public static class Dashboard
    {
        public const string Default = GroupName + ".Dashboard";
        public const string Student = GroupName + ".StoryBook.Student";
        public const string Instructor = GroupName + ".StoryBook.Instructor";
        public const string UpdateProfile = GroupName + ".UpdateProfile";
    }    
    
    //this is for exam module
    public static class ExamQuestion
    {
        public const string Default = GroupName + ".ExamQuestion";
        public const string SeeExamResult = GroupName + ".SeeExamResult";
        public const string GiveExam = GroupName + ".GiveExam";
    }

}
