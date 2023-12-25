$(function () {
    var l = abp.localization.getResource('Learn');
    var createModal = new abp.ModalManager(abp.appPath + 'Students/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'Students/EditModal');
    var approveModal = new abp.ModalManager(abp.appPath + 'Students/ApproveModal');
    var resetPasswordModal = new abp.ModalManager(abp.appPath + 'Students/ResetPasswordModal');

    var dataTable = $('#StudentsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(kidoo.learn.students.student.getList, function () {
                return {
                    filter: $("#Input_Filter").val(),
                    filterReferral: $("#Input_FilterReferral").val(),
                    filterGender: $("#Input_FilterGender").val(),
                    filterAgeGroup: $("#Input_FilterAgeGroup").val(),
                    filterDistrict: $("#Input_FilterDistrict").val(),
                    filterPaymentStatus: $("#Input_FilterPaymentStatus").val(),
                };
            }),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Approve'),
                                    visible:
                                        abp.auth.isGranted('Learn.Students.Approve'),
                                    action: function (data) {
                                        approveModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Edit'),
                                    visible:
                                        abp.auth.isGranted('Learn.Students.Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                }, 
                                {
                                    text: l('Delete'),
                                    visible:
                                        abp.auth.isGranted('Learn.Students.Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'StudentDeletionConfirmationMessage',
                                            data.record.firstName,
                                        );
                                    },
                                    action: function (data) {
                                        kidoo.learn.students.student
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
                    title: l('First Name'),
                    data: "firstName"
                },
                {
                    title: l('Last Name'),
                    data: "lastName"
                },
                {
                    title: l('Email Address'),
                    data: "emailAddress"
                },
                {
                    title: l('Phone Number'),
                    data: "phoneNumber"
                },
                {
                    title: 'Age Group',
                    data: 'ageGroup',
                    render: function (data) {
                        switch (data) {
                            case 1:
                                return l('Enum:SevenToEleven');
                            case 2:
                                return l('Enum:BelowSeven');
                            default:
                                return 'Unknown';
                        }
                    }
                },
                {
                    title: l('Transaction Id'),
                    data: "transactionId"
                },
                {
                    title: 'Payment Status',
                    data: 'paymentStatus',
                    render: function (data, type, row) {
                        var statusText = '';
                        var statusClass = '';

                        switch (data) {
                            case 1:
                                statusText = l('Enum:Pending');
                                statusClass = 'badge rounded-pill bg-warning';
                                break;
                            case 2:
                                statusText = l('Enum:Approved');
                                statusClass = 'badge rounded-pill bg-success';
                                break;
                            case 3:
                                statusText = l('Enum:Rejected');
                                statusClass = 'badge rounded-pill bg-danger';
                                break;
                            default:
                                statusText = 'Unknown';
                        }

                        // Apply the class to the cell
                        return '<span class="' + statusClass + '">' + statusText + '</span>';
                    }
                },
                {
                    title: l('Enrollment Date'),
                    data: "enrollmentDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString();
                    }
                }
            ]
        })
    );

    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    resetPasswordModal.onResult(function (e) {
        dataTable.ajax.reload();
    });

    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
    approveModal.onResult(function () {
        dataTable.ajax.reload();
    });

    $('#NewStudentButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });

    $("#filterbtn").click(function () {
        dataTable.ajax.reload();
    });
});
