$(function () {
    var l = abp.localization.getResource('Learn');
    var createModal = new abp.ModalManager(abp.appPath + 'CourseSectionTopics/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'CourseSectionTopics/EditModal');
    var courseId = $('#CourseId').val();
    var sectionId = $('#SectionId').val();
    console.log(courseId);


    var dataTable = $('#CourseSectionTopicTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: true,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(function (data) {
                return kidoo.learn.courses.course.getCourseSectionTopicList(courseId, sectionId);
            }),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id, courseId: courseId, sectionId: sectionId });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'CourseSectionTopicDeletionConfirmationMessage'
                                        );
                                    },
                                    action: function (data) {
                                        kidoo.learn.courses.course
                                            .deleteTopic(data.record, courseId, sectionId, data.record.id)
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
                    title: l('Title'),
                    data: "title"
                },
                {
                    title: l('Video Duration'),
                    data: "videoDurationInMinutes"
                },
                {
                    title: l('Thumbnil Url'),
                    data: "thumbnailUrl"
                },
                {
                    title: l('Video Url'),
                    data: "videoUrl"
                },

            ]
        })
    );

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCourseSectionTopicButton').click(function (e) {
        e.preventDefault();
        createModal.open({ courseId, sectionId });
    });
});
