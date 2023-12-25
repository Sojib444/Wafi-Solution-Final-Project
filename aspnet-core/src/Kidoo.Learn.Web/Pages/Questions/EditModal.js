$(document).ready(function () {
    toggleQuestionGroupVisibility();

    var selectedQuestionType = $("#EditQuestionDto_QuestionType").val();
    togglemultipleChoiceOptionsVisibility();

    $("#EditQuestionDto_Subject").change(function () {
        debugger
        toggleQuestionGroupVisibility();
    });

    function toggleQuestionGroupVisibility() {
        var selectedSubject = $("#EditQuestionDto_Subject").val();
        if (selectedSubject === "1") {
            $("#EditQuestionDto_StoryGroup").closest(".form-group").show();
        } else {
            $("#EditQuestionDto_StoryGroup").closest(".form-group").hide();
        }
    }
    $("#EditQuestionDto_QuestionType").change(function () {
        debugger 
        togglemultipleChoiceOptionsVisibility()
    });
    function togglemultipleChoiceOptionsVisibility() {
        selectedQuestionType = $("#EditQuestionDto_QuestionType").val();
        var containsMultipleChoice = selectedQuestionType.includes(1);
        debugger
        $("#multipleChoiceOptions").toggle(containsMultipleChoice);
    }
     
}); 