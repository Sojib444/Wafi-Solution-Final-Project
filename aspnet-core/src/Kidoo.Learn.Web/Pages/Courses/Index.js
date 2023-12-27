$(function () {
    var l = abp.localization.getResource('Learn');
    var createModal = new abp.ModalManager(abp.appPath + 'Courses/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Courses/EditModal');

    var dataTable = $('#CoursesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(kidoo.learn.courses.course.getList),
            columnDefs: [ 
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'CourseDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        console.log(data.record.id);
                                        kidoo.learn.courses.course
                                            .deleteCourse(data.record.id)
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
                    title: l('Description'),
                    data: "description"
                },
                {
                    title: l('Number Of Lectures'),
                    data: "numberOfLectures"
                },
                {
                    title: l('VideoDuration'),
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

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewCourseButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});
