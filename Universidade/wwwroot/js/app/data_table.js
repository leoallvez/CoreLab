$(document).ready(function () {
    $('.table_list').DataTable({
        "order": [[1, "asc"]],
        "language": {
            "sEmptyTable": "Nenhum registro encontrado",
            "sInfo": "Mostrando de _START_ at&aacute; _END_ de _TOTAL_ registros",
            "sInfoEmpty": "Mostrando 0 at&eacute; 0 de 0 registros",
            "sInfoFiltered": "(Filtrados de _MAX_ registros)",
            "sInfoPostFix": "",
            "sInfoThousands": ".",
            "sLengthMenu": "_MENU_ Por p&aacute;gina",
            "searchPlaceholder": "Pesquisar",
            "sLoadingRecords": "Carregando...",
            "sProcessing": "Processando...",
            "sZeroRecords": "Nenhum registro encontrado",
            "sSearch": "",
            "oPaginate": {
                "sNext": "Pr&oacute;ximo",
                "sPrevious": "Anterior",
                "sFirst": "Primeiro",
                "sLast": "Último"
            },
            "oAria": {
                "sSortAscending": ": Ordenar colunas de forma ascendente",
                "sSortDescending": ": Ordenar colunas de forma descendente"
            }
        }
    });
});
