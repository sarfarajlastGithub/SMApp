$(document)
    .ready(function() {
        //Prepare jtable plugin
        $('#StudentTableContainer')
            .jtable({
                title: 'Student List',
                paging: false,
                sorting: true,
                defaultSorting: 'Name ASC',
                selecting: true, //Enable selecting
                multiselect: true, //Allow multiple selecting
                selectingCheckboxes: true, //Show checkboxes on first column
                //selectOnRowClick: false, //Enable this to only select using checkboxes
                actions: {
                    // listAction: '@Url.Action("StudentListByFiter")'
                    listAction: '/Attendance/StudentListByFiter'
                },
                fields: {
                    RegId: {
                        key: true,

                        list: false
                    },
                    Name: {
                        title: 'Name',
                        width: '23%',
                        inputClass: 'validate[required]'
                    },
                    Sclass: {
                        title: 'Class',
                        width: '23%',
                        inputClass: 'validate[required]'
                    },
                    Ssection: {
                        title: 'Section',
                        width: '23%',
                        inputClass: 'validate[required]'
                    },
                    RolNo: {
                        title: 'Roll',
                        width: '23%',
                        inputClass: 'validate[required]'
                    }
                },
                //Register to selectionChanged event to hanlde events
                selectionChanged: function() {
                    //Get all selected rows
                    var $selectedRows = $('#StudentTableContainer').jtable('selectedRows');

                    $('#SelectedRowList').empty();
                    if ($selectedRows.length > 0) {
                        //Show selected rows
                        $selectedRows.each(function() {
                            var record = $(this).data('record');
                            $('#SelectedRowList')
                                .append(
                                    '<b>Name: </b>: ' +
                                    record.Name +
                                    '<b> Class:</b>:' +
                                    record.Sclass +
                                    '<b> Section:</b>:' +
                                    record.Ssection +
                                    '<br /><br />:' +
                                    record.RolNo +
                                    '<br /><br />'
                                );
                        });
                    } else {
                        //No rows selected
                        $('#SelectedRowList').append('No Student selected! Select rows to see here...');
                    }
                }
            });

        //Re-load records when user click 'load records' button.
        $('#LoadRecordsButton')
            .click(function(e) {
                e.preventDefault();
                var sClass = $('#Stuclass').val();
                var sSection = $('#StuSection').val();

                if (sClass === "0" || sSection === "0") {
                    alert("Atleast Select 2.Class and 3.Section");
                    return;
                }
                $('#StudentTableContainer')
                    .jtable('load',
                    {
                        Stuclass: $('#Stuclass').val(),
                        Name: $('#Name').val(),
                        StuSection: $('#StuSection').val()
                    });
            });

        //Load all records when page is first shown
        //$('#LoadRecordsButton').click();

        //Load student list from server
        //$('#StudentTableContainer').jtable('load');

        //Present selected students
        $('#PresentButton')
            .button()
            .click(function() {
                var date = $("#SelecteDateTime").val();
                var pStatus = $('#PresentStatus').val();
                if (date === "" || pStatus === "") {
                    alert("" +
                        "Select Date And Status");
                    return;
                }

                var $selectedRows = $('#StudentTableContainer').jtable('selectedRows');

                var masterStudent = {
                    pdate: $('#SelecteDateTime').val(),
                    presentStatus: $('#PresentStatus').val(),
                    Ids: ""
                }

                var studentIdName = [];
                if ($selectedRows.length > 0) {
                    //Show selected rows
                    $selectedRows.each(function() {
                        var record = $(this).data('record');
                        //contact.ContactID.push(record.RegId);
                        var item = {};
                        item["id"] = record.RegId;
                        item["name"] = record.Name;
                        item["tyear"] = record.TenureName;
                        item["stuClass"] = record.Sclass;
                        item["stuSection"] = record.Ssection;
                        studentIdName.push(item);
                    });
                    //JSON.stringify(studentIdName) this for Converting JSON to string to send data as a string
                    masterStudent.Ids = JSON.stringify(studentIdName);
                    //Present Student
                    $.ajax({
                        url: '/Attendance/PresentStudent',
                        type: 'POST',
                        data: masterStudent,
                        dataType: 'json',
                        success: function(data) {
                            alert(data.message);
                            //if (data.status) {

                            //}
                        },
                        error: function() {
                            $('#msg').html('<div class="failed">Error! Please try again.</div>');
                        }
                    });
                    //console.log(studentIdName);

                } else {
                    //No rows selected
                    $('#SelectedRowList').html('No Student selected! Select rows to see here...');
                }

                //$('#StudentTableContainer').jtable('deleteRows', $selectedRowss);
                //$('#jesndata').html($selectedRowss.serializeArray());

            });

        //End of jTable
    });