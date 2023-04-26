$(() => {

    console.log("in java script!")

    function refreshTable() {
        $('tbody').empty();
        $.get('/home/getpeople', function (people) {
            people.forEach(function (person) {
                $('tbody').append(
                    `<tr>
            <td>${person.firstName}</td>
            <td>${person.lastName}</td>
            <td>${person.age}</td>
            <td><button class="btn btn-warning" data-edit-id=${person.id}>Edit</button></td>
            <td><button class="btn btn-danger" data-delete-id=${person.id}>Delete</button></td>
       </tr>`)
            });
        });
    }

    refreshTable();

    const modal = new bootstrap.Modal($('.modal')[0]);
    $("#add-person").on('click', function () {
        reset();
        modal.show();

    })

    $(".modal").on('click','#save-person', function () {
        const firstName = $('#firstName').val()
        const lastName = $('#lastName').val()
        const age = $('#age').val()

        $.post('/home/addperson', { firstName, lastName, age }, function () {
            modal.hide();
            refreshTable();
        })

    })


    $("tbody").on('click', '.btn-danger', function () {
        console.log("in delete!")

        const id = $(this).data('delete-id');

        $.post('home/deleteperson', { id }, function () {
            refreshTable();
        })
    });



    let editId;

    /*EDIT!!!*/

    $("tbody").on('click', '.btn-warning', function () {
        console.log("in edit!!!")

        editId = $(this).data('edit-id')
        $.post('home/getpersontoedit', { editId }, function (person) {
            $("#firstName").val(person.firstName);
            $("#lastName").val(person.lastName);
            $("#age").val(person.age);
            $('.modal-title').text('Edit Person')
            $('#save-person').attr('id', 'update-person')
            $('#update-person').text('Update')
            modal.show();
        })



    });


    function reset() {
        $("#firstName").val('');
        $("#lastName").val('');
        $("#age").val('');
        $('#update-person').attr('id', 'save-person')
        $('#save-person').text('Save')
        $('.modal-title').text('Add Person')
    }


    $('.modal').on('click', '#update-person', function () {
        console.log("update")

        const firstName = $('#firstName').val()
        const lastName = $('#lastName').val()
        const age = $('#age').val()
        const id = editId;


        $.post('/home/updateperson', { firstName, lastName, age, id }, function () {
            modal.hide();
            refreshTable();
        })
    })






})