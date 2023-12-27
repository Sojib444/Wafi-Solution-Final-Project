$(function () {
    var l = abp.localization.getResource('Learn');

    var dataTable = $('#CoursesTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(kidoo.learn.courses.course.getList),
            columnDefs: [                                    
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
});
