function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

function AreYouSureDeleteMeeting(x) {
    var meetingID = parseInt(x.parentNode.parentNode.childNodes[1].textContent);
    var meetingName = x.parentNode.parentNode.childNodes[3].textContent;
    const wrapper = document.createElement('div');
    wrapper.innerHTML = "<span style='font-weight:bold;color:red;'>" + meetingName + "</span> başlıklı toplantınızı silmek istediğinize emin misiniz ?";
    swal({
        title: "Silme İşlemi",
        content: wrapper,
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("İşlem başarılı toplantınız 3 saniye sonra silinecektir!. Lütfen bekleyiniz.", {
                    icon: "success",
                    buttons:false
                });
                setTimeout(() => { window.location.href = "/Meeting/DeleteMeeting/" + meetingID;}, 3000);
               
                    
            } else {
                swal("İşlem iptal edildi toplantınız güvende!", {
                    icon:"info"
                });
            }
        });
}


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}

function AreYouSureSendMail(x) {
    var form = x.parents('form');
    swal({
        title: "Silme İşlemi",
        content: wrapper,
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((willAdd) => {
            if (willAdd) {
                swal("Mailler onaylandı 3 saniye sonra toplantı eklenecektir!. Lütfen bekleyiniz.", {
                    icon: "success",
                    buttons: false
                });



            } else {
                swal("İşlem iptal edildi toplantınız güvende!", {
                    icon: "info"
                });
            }
        });
}

function AreYouSureVoteMeeting(email, chosedDate,chosedDateText,meetingID) {
    const wrapper = document.createElement('div');
    wrapper.innerHTML = "<span style = 'font-weight:bold;color:red;'> " + chosedDateText + "</span > tarih ve saatini seçmek istediğinize emin misiniz ?<br/><br/>" +
        "<form id='voteForm' method='post' action='/Meeting/VoteAMeeting'><input name='email' type='text' value='" + email + "' style='display:none;'></input><input name='dateIndex' type='number' value='" + chosedDate + "' style='display:none;'></input><input name='meetingID' type='number' value='" + meetingID +"' style='display:none;'></input></form> ";
    swal({
        title: "Oylama İşlemi",
        content: wrapper,
        icon: "warning",
        buttons: true,
        dangerMode: false,
    })
        .then((willVote) => {
            if (willVote) {
                var valdata = $("#voteForm").serialize();
                $.ajax({
                    url: "/Meeting/VoteAMeeting",
                    type: "POST",
                    dataType: 'json',
                    contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                    data: valdata,
                    success: function () {
                        swal("İşlem başarılı oylamanız gerçekleşmiştir. Sayfayı kapatabilirsiniz.", {
                            icon: "success",
                            buttons: false
                        });
                    },
                    error: function () {
                        swal("İşleminiz gerçekleşmemiştir lütfen sonra tekrar deneyiniz.", {
                            icon: "error",
                            buttons: false
                        });
                    }
                });
            } else {
                swal("Oylama işlemi iptal edildi!", {
                    icon: "info"
                });
            }
        });
}