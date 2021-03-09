(function () {
    var hasError = false;

    $('.table').dataTable({
        'language': {
            'emptyTable': 'Nenhum registro encontrado',
            'lengthMenu': 'Exibir _MENU_ resultados por página',
            'search': 'Pesquisar',
            'paginate': { 'previous': 'Anterior', 'next': 'Próximo' },
            'zeroRecords': 'Nenhum registro encontrado',
            'info': 'Mostrando de _START_ até _END_ de _TOTAL_ registros',
            'infoEmpty': 'Mostrando 0 até 0 de 0 registros',
            'infoFiltered': '(Filtrados de _MAX_ registros)',
            'infoThousands': '.',
            'loadingRecords': 'Carregando...',
            'processing': 'Processando...',
            'thousands': '.'
        },
        'processing': true,
        'serverSide': true,
        'destroy': true,
        'order': [[0, 'asc']],
        'lengthMenu': [5, 10, 25, 50, 100],
        'pageLength': 5,
        'ordering': false,
        'ajax': {
            'url': '/Cliente/GetClientes',
            'type': 'POST',
            'datatype': 'json',
            'complete': function (data) {
                $('[data-toggle="tooltip"]').tooltip();
            }
        },
        'columns': [
            { 'data': 'ClienteId', 'autoWidth': true },
            { 'data': 'NomeCliente', 'autoWidth': true },
            { 'data': 'TipoCliente', 'autoWidth': true },
            { 'data': 'NomeContato', 'autoWidth': true },
            { 'data': 'TelefoneContato', 'autoWidth': true },
            { 'data': 'Cidade', 'autoWidth': true },
            { 'data': 'Bairro', 'autoWidth': true },
            { 'data': 'Logradouro', 'autoWidth': true },
            { 'data': 'DataCadastro', 'autoWidth': true },
            { 'data': 'DataAtualizacao', 'autoWidth': true },
            {
                'data': 'ClienteId',
                'render': function (data) {
                    return '<button type="button" class="btn btn-warning btn-xs btn-editar" data-id="' + data + '" data-toggle="tooltip" data-placement="top" title="Editar"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button>';
                },
                'searchable': false,
                'width': '2.4%'
            },
            {
                'data': 'ClienteId',
                'render': function (data) {
                    return '<button type="button" class="btn btn-danger btn-xs btn-excluir" data-id="' + data + '" data-toggle="tooltip" data-placement="top" title="Excluir"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span></button>';
                },
                'searchable': false,
                'width': '2.4%'
            }
        ]
    });

    $('ul.nav.navbar-nav li').removeClass('active');
    $('#nav-cliente').addClass('active');

    $('.btn-novo').on('click', function (evt) {
        $('#modal-add-upd .modal-title').text('Novo Cliente');
        $('#ClienteId').val('0');
    });

    $('.table').on('click', '.btn-editar', function (evt) {
        var ClienteId = $(this).data('id');
        $('#ClienteId').val(ClienteId);

        $.ajax({
            'url': '/Cliente/Find/' + ClienteId,
            'type': 'POST',
            'contentType': 'application/json; charset=utf-8',
            'dataType': 'json',
            'success': function (data) {
                $('#CodigoId').val(data.CodigoId);
                $('#NomeCliente').val(data.NomeCliente),
                $('#TipoCliente').val(data.TipoCliente),
                $('#NomeContato').val(data.NomeContato),
                $('#TelefoneContato').val(data.TelefoneContato),
                $('#Cidade').val(data.Cidade),
                $('#Bairro').val(data.Bairro),
                $('#Logradouro').val(data.Logradouro)
                $('#modal-add-upd .modal-title').text('Atualizar Cliente');
                $('#modal-add-upd').modal('show');
            },
            'error': function (error) {
                console.log(error);
            }
        });
    });

    $('.table').on('click', '.btn-excluir', function (evt) {
        var ClientId = $(this).data('id');

        $('.action-delete').data('id', ClientId);
        $('#modal-excluir').modal('show');
    });

    $('.modal-footer').on('click', '.action-save', function (evt) {
        var ClienteId = parseInt($('#ClienteId').val()),
            cliente = {
                ClienteId: ClienteId,
                NomeCliente: $('#NomeCliente').val(),
                TipoCliente: $('#TipoCliente').val(),
                NomeContato: $('#NomeContato').val(),
                TelefoneContato: $('#TelefoneContato').val(),
                Cidade: $('#Cidade').val(),
                Bairro: $('#Bairro').val(),
                Logradouro: $('#Logradouro').val()
            };
                
        if (!ClienteId) {
            $.ajax({
                'url': '/Cliente/Add',
                'data': JSON.stringify({cliente: cliente}),
                'type': 'POST',
                'contentType': 'application/json; charset=utf-8',
                'dataType': 'json',
                'success': function (data) {
                    $('#frm-add-upd').trigger("reset");
                    $('#modal-add-upd').modal('hide');
                    $('.table').DataTable().draw(false);
                },
                'error': function (error) {
                    hasError = true;
                    $('#modal-add-upd').modal('hide');
                    $('#modal-error .modal-body').html(error.responseText.replace(/-/ig, '<br>'));
                    $('#modal-error').modal('show');
                }
            });
        }
        else {
            $.ajax({
                'url': '/Cliente/Update',
                'data': JSON.stringify({ cliente: cliente }),
                'type': 'POST',
                'contentType': 'application/json; charset=utf-8',
                'dataType': 'json',
                'success': function (data) {
                    $('#frm-add-upd').trigger('reset');
                    $('#modal-add-upd').modal('hide');
                    $('.table').DataTable().draw(false);
                },
                'error': function (error) {
                    hasError = true;
                    $('#modal-add-upd').modal('hide');
                    $('#modal-error .modal-body').html(error.responseText.replace(/-/ig, '<br>'));
                    $('#modal-error').modal('show');
                }
            });
        }
    });

    $('.modal-footer').on('click', '.action-delete', function (evt) {
        $.ajax({
            'url': '/Cliente/Delete/' + $(this).data('id'),
            'type': 'POST',
            'contentType': 'application/json; charset=utf-8',
            'dataType': 'json',
            'success': function (data) {
                $('#modal-excluir').modal('hide');
                $('.table').DataTable().draw(false);
            },
            'error': function (error) {
                $('#modal-excluir').modal('hide');
                $('#modal-error .modal-body').html(error.responseText.replace(/-/ig, '<br>'));
                $('#modal-error').modal('show');
            }
        });
    });

    $('#modal-add-upd').on('hidden.bs.modal', function (evt) {
        if (!hasError) {
            $('#frm-add-upd').trigger('reset');
        }
    });

    $('#modal-error').on('hidden.bs.modal', function (evt) {
        hasError = false;
        $('#modal-add-upd').modal('show');
    });
})();