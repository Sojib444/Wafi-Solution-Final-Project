using Kidoo.Learn.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Kidoo.Learn.Permissions;

public class LearnPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        #region Student permission
        var studentGroupName = context.AddGroup("Students", L("Students"));
        var studentPermission = studentGroupName.AddPermission(LearnPermissions.Students.Default, L("Students"));
        studentPermission.AddChild(LearnPermissions.Students.Create, L("Create"));
        studentPermission.AddChild(LearnPermissions.Students.Edit, L("Edit"));
        studentPermission.AddChild(LearnPermissions.Students.Delete, L("Delete"));
        studentPermission.AddChild(LearnPermissions.Students.Approve, L("Approve"));
        studentPermission.AddChild(LearnPermissions.Students.ResetPassword, L("ResetPassword"));
        #endregion

        #region Course permission
        var courseGroupName = context.AddGroup("Courses", L("Courses"));
        var coursePermission = courseGroupName.AddPermission(LearnPermissions.Courses.Default, L("Courses"));
        coursePermission.AddChild(LearnPermissions.Courses.Create, L("Create"));
        coursePermission.AddChild(LearnPermissions.Courses.Edit, L("Edit"));
        coursePermission.AddChild(LearnPermissions.Courses.Delete, L("Delete"));
        #endregion

        #region Question permission
        var questionGroupName = context.AddGroup("Questions", L("Questions"));
        var questionPermission = questionGroupName.AddPermission(LearnPermissions.Question.Default, L("Questions"));
        questionPermission.AddChild(LearnPermissions.Question.Create, L("Create"));
        questionPermission.AddChild(LearnPermissions.Question.Edit, L("Edit"));
        questionPermission.AddChild(LearnPermissions.Question.Delete, L("Delete"));
        #endregion

        #region Exam permission
        var examGroupName = context.AddGroup("Exams", L("Exams"));
        var examPermission = examGroupName.AddPermission(LearnPermissions.Exam.Default, L("Exams"));
        examPermission.AddChild(LearnPermissions.Exam.Create, L("Create"));
        examPermission.AddChild(LearnPermissions.Exam.Edit, L("Edit"));
        examPermission.AddChild(LearnPermissions.Exam.Delete, L("Delete"));
        examPermission.AddChild(LearnPermissions.Exam.PublishResult, L("Publish Result"));
        examPermission.AddChild(LearnPermissions.Exam.PublishExam, L("Publish Exam"));
        #endregion

        #region ExamResult permission
        var examResultGroupName = context.AddGroup("ExamResults", L("Results"));
        var examResultPermission = examResultGroupName.AddPermission(LearnPermissions.ExamResult.Default, L("Exams"));
        examResultPermission.AddChild(LearnPermissions.ExamResult.Delete, L("Delete"));
        examResultPermission.AddChild(LearnPermissions.ExamResult.ReSubmission, L("ReSubmission"));
        examResultPermission.AddChild(LearnPermissions.ExamResult.Edit, L("Edit"));
        examResultPermission.AddChild(LearnPermissions.ExamResult.Answers, L("Answers"));
        examResultPermission.AddChild(LearnPermissions.ExamResult.ApproveAllExam, L("ApproveAllExam"));
        examResultPermission.AddChild(LearnPermissions.ExamResult.ApproveAllAnswer, L("ApproveAllAnswer"));
        #endregion

        #region Exam Answer permission
        var examAnsGroupName = context.AddGroup("Answers", L("Answers"));
        var examAnswerPermission = examAnsGroupName.AddPermission(LearnPermissions.ExamAnswer.Default, L("Answers"));
        examAnswerPermission.AddChild(LearnPermissions.ExamAnswer.Approve, L("Approve"));
        examAnswerPermission.AddChild(LearnPermissions.ExamAnswer.Edit, L("Edit"));
        #endregion

        #region Story Book
        var bookGroupName = context.AddGroup("Books", L("Books"));
        var bookPermission = bookGroupName.AddPermission(LearnPermissions.StoryBook.Default, L("Books"));
        bookPermission.AddChild(LearnPermissions.StoryBook.Bangla, L("Bangla"));
        bookPermission.AddChild(LearnPermissions.StoryBook.English, L("English"));
        #endregion

        #region Dashboard permission
        var dashboardGroupName = context.AddGroup("Dashboards", L("Dashboards"));
        var dashboardPermission = dashboardGroupName.AddPermission(LearnPermissions.Dashboard.Default, L("Dashboard"));
        dashboardPermission.AddChild(LearnPermissions.Dashboard.Student, L("Student"));
        dashboardPermission.AddChild(LearnPermissions.Dashboard.Instructor, L("Instructor"));
        dashboardPermission.AddChild(LearnPermissions.Dashboard.UpdateProfile, L("UpdateProfile"));
        #endregion

        #region This is for give exam module
        var examQuestionGroup = context.AddGroup("ExamQuestions", L("Exam-Questions"));
        var examQuestionPermission = examQuestionGroup.AddPermission(LearnPermissions.ExamQuestion.Default, L("ExamQuestions"));
        examQuestionPermission.AddChild(LearnPermissions.ExamQuestion.GiveExam, L("GiveExam"));
        examQuestionPermission.AddChild(LearnPermissions.ExamQuestion.SeeExamResult, L("SeeExamResult"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<LearnResource>(name);
    }
}
