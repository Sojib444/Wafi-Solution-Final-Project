$(function () {
    var l = abp.localization.getResource('Learn');
    var createModal = new abp.ModalManager(abp.appPath + 'CourseSections/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'CourseSections/EditModal');
    var courseId = $('#CourseId').val();
    console.log(courseId);


    var dataTable = $('#CourseSectionsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(function (data) {
                return kidoo.learn.courses.course.getCouresSectionsList(courseId);
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
                                        editModal.open({ id: data.record.id, courseId: courseId });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'CourseSectionDeletionConfirmationMessage'
                                        );
                                    },
                                    action: function (data) {
                                        kidoo.learn.courses.course
                                            .deleteSection(data.record, courseId, data.record.id)
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
                    title: l('Min Age'),
                    data: "minAge"
                },
                {
                    title: l('Max Age'),
                    data: "maxAge"
                },
                {
                    title: l('Thumbnil Url'),
                    data: "thumbnailUrl"
                }
            ]
        })
    );

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCourseSectionButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});



