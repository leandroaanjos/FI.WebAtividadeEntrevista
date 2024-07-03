$(document).ready(function () {    
    if (document.getElementById("gridBeneficiarios"))
        carregaGridBeneficiarios();
})

function openBeneficiariosModalDialog() {
    $('#modalBeneficiarios').modal('show');
}

$('#formBeneficiario').submit(function (e) {
    e.preventDefault();

    let cpf = $('#BeneficiarioCPF').val();
    let nome = $('#BeneficiarioNome').val();

    if (cpf && nome) {
        // validação do CPF
        let regCpf = /^\d{3}\.\d{3}\.\d{3}\-\d{2}$/;
        if (!regCpf.test(cpf)) {
            AlertModalDialog("Validação", "CPF inválido!");
            return;
        }

        let item = { CPF: cpf, Nome: nome };

        if (beneficiariosList.some(x => x.CPF === cpf)) {
            beneficiariosList = beneficiariosList.map(x => x.CPF === item.CPF ? { ...x, Nome: item.Nome } : x);
        }
        else {
            beneficiariosList = beneficiariosList.concat(item);
        }

        carregaGridBeneficiarios();

        $('#BeneficiarioCPF').val('');
        $('#BeneficiarioNome').val('');
    }
    else {
        AlertModalDialog("Validação", "Por favor, preencha todos os campos!");
    }
})

function carregaGridBeneficiarios() {
    $('#gridBeneficiarios tbody').html("");

    let list = beneficiariosList;

    for (let i = 0; i < list.length; i++) {
        let item = list[i];
        let linhaHTML = `
            <tr class="rowClass${onlyNumbers(item.CPF)}"> 
                <td>${item.CPF}</td>
                <td>${item.Nome}</td>
                <td>
                    <button class="btn btn-primary" onclick="selecionaBeneficiario('${item.CPF}')">Alterar</button>
                    <button class="btn btn-danger" onclick="excluiBeneficiario('${item.CPF}')">Excluir</button>
                </td>
            </tr>`;
        $('#gridBeneficiarios tbody').append(linhaHTML);
    }
}

function excluiBeneficiario(cpf) {
    beneficiariosList.splice(beneficiariosList.findIndex(function (item) {
        return item.CPF === cpf;
    }), 1);
    $('tr.rowClass' + onlyNumbers(cpf)).remove();
}

function selecionaBeneficiario(cpf) {
    let beneficiario = beneficiariosList.find(x => x.CPF === cpf);

    if (document.getElementById("BeneficiarioCPF"))
        $('#BeneficiarioCPF').val(beneficiario.CPF);

    if (document.getElementById("BeneficiarioNome"))
        $('#BeneficiarioNome').val(beneficiario.Nome);
}

function AlertModalDialog(titulo, texto) {
    var random = Math.random().toString().replace('.', '');
    var texto = '<div id="' + random + '" class="modal fade">                                                               ' +
        '        <div class="modal-dialog">                                                                                 ' +
        '            <div class="modal-content">                                                                            ' +
        '                <div class="modal-header">                                                                         ' +
        '                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>         ' +
        '                    <h4 class="modal-title">' + titulo + '</h4>                                                    ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-body">                                                                           ' +
        '                    <p>' + texto + '</p>                                                                           ' +
        '                </div>                                                                                             ' +
        '                <div class="modal-footer">                                                                         ' +
        '                    <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>             ' +
        '                                                                                                                   ' +
        '                </div>                                                                                             ' +
        '            </div><!-- /.modal-content -->                                                                         ' +
        '  </div><!-- /.modal-dialog -->                                                                                    ' +
        '</div> <!-- /.modal -->                                                                                        ';

    $('body').append(texto);
    $('#' + random).modal('show');
}

function onlyNumbers(str) {
    if (!str) {
        return str;
    }
    return str.replace(/\D+/g, '');
}