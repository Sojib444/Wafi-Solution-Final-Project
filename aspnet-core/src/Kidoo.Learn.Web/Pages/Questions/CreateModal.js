$(document).ready(function () {
    toggleQuestionGroupVisibility();

    var selectedQuestionType = $("#QuestionDto_QuestionType").val();
    togglemultipleChoiceOptionsVisibility();

    $("#QuestionDto_Subject").change(function () {
        debugger
        toggleQuestionGroupVisibility();
    });

    function toggleQuestionGroupVisibility() {
        var selectedSubject = $("#QuestionDto_Subject").val();
        if (selectedSubject === "1") {
            $("#QuestionDto_StoryGroup").closest(".form-group").show();
        } else {
            $("#QuestionDto_StoryGroup").closest(".form-group").hide();
        }
    }
    $("#QuestionDto_QuestionType").change(function () {
        debugger
        togglemultipleChoiceOptionsVisibility()
    });
    function togglemultipleChoiceOptionsVisibility() {
        selectedQuestionType = $("#QuestionDto_QuestionType").val();
        var containsMultipleChoice = selectedQuestionType.includes(1);
        debugger
        $("#multipleChoiceOptions").toggle(containsMultipleChoice);
    }

}); 