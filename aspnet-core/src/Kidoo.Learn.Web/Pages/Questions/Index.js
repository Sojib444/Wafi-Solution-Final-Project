$(function () {
    var l = abp.localization.getResource('Learn');
    var createModal = new abp.ModalManager(abp.appPath + 'Questions/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Questions/EditModal');

    var dataTable = $('#QuestionTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(kidoo.learn.questions.question.getList, function () {
                return {
                    subject: $("#Input_Subject").val(),
                    questionGroup: $("#Input_StoryGroup").val(),
                    difficultyLevel: $("#Input_DifficultyLevel").val(),
                };
            }),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('Learn.Questions.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('Learn.Questions.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'QuestionDeletionConfirmationMessage',
                                            data.record.questionUniqueId,
                                        );
                                    },
                                    action: function (data) {
                                        kidoo.learn.questions.question
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Unique QuestionId'),
                    data: "questionUniqueId"
                },
                {
                    title: l('Title'),
                    data: "title",
                    render: function (data) {
                        return data.length > 35 ? data.substring(0, 35) + '...' : data;
                    }
                },
                {
                    title: l('Subject'),
                    data: "subject",
                    render: function (data) {
                        return l('Enum:Subject:' + data);
                    }
                },
                {
                    title: l('Story Group'),
                    data: "storyGroup",
                    render: function (data) {
                        return l('Enum:StoryGroup:' + data);
                    }

                },
                {
                    title: l('QuestionType'),
                    data: "questionType",
                    render: function (data) {
                        return l('Enum:QuestionType:' + data);
                    }
                },
                {
                    title: l('Difficulty Level'),
                    data: "difficultyLevel",
                    render: function (data) {
                        return l('Enum:DifficultyLevel:' + data);
                    }
                },
                {
                    title: l('Text Answer'),
                    data: "correctAnswer",
                    render: function (data) {
                        return data == null ? null : data.length > 20 ? data.substring(0, 20) + '...' : data;
                    }
                },
                {
                    title: l('M-C Answer'),
                    data: "correctOption",
                },
                {
                    title: l('Question-Options'),
                    data: "options",
                    render: function (data) {
                        debugger
                        if (data != null && data.length > 0) {
                            var optionTexts = data.map(function (option) {
                                return option.optionText;
                            });
                            return optionTexts.join(' | ');
                        } else {
                            return '';
                        }
                    }
                }

            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload(null, false);
    });

    $('#NewQuestionButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
    $("#filterbtn").click(function () {
        console.log("filterbtn clicked");
        dataTable.ajax.reload();
    });
});
