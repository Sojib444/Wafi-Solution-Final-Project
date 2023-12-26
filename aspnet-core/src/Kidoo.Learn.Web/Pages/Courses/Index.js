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
                    title: l('Thumb Nill'),
                    data: "thumbnailUrl"
                }
            ]
        })
    );
});
