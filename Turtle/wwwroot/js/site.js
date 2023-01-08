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
        buttons: true, buttons: ["İptal Et", "Onayla"],
        dangerMode: true,
    })
        .then((willDelete) => {
            if (willDelete) {
                swal("İşlem başarılı toplantınız 3 saniye sonra silinecektir!. Lütfen bekleyiniz.", {
                    icon: "success",
                    buttons: false
                });
                setTimeout(() => { window.location.href = "/Meeting/DeleteMeeting/" + meetingID; }, 3000);


            } else {
                swal("İşlem iptal edildi toplantınız güvende!", {
                    icon: "info"
                });
            }
        });
}


function formatNumber(n) {
    // format number 1000000 to 1,234,567
    return n.replace(/\D/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ".")
}

function AreYouSureVoteMeeting(email, chosedDate, chosedDateText, meetingID) {
    const wrapper = document.createElement('div');
    wrapper.innerHTML = "<span style = 'font-weight:bold;color:red;'> " + chosedDateText + "</span > tarih ve saatini seçmek istediğinize emin misiniz ?<br/><br/>" +
        "<form id='voteForm' method='post' action='/Meeting/VoteAMeeting'><input name='email' type='text' value='" + email + "' style='display:none;'></input><input name='dateIndex' type='number' value='" + chosedDate + "' style='display:none;'></input><input name='meetingID' type='number' value='" + meetingID + "' style='display:none;'></input></form> ";
    swal({
        title: "Oylama İşlemi",
        content: wrapper,
        icon: "warning",
        buttons: true, buttons: ["İptal Et", "Onayla"],
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


function DoYouUpdateMeeting() {
    const wrapper = document.createElement('div');
    wrapper.innerHTML = "Toplantı bilgilerini güncellemek istediğinize emin misiniz ?<br/><br/>";
    //"<form id='voteForm' method='post' action='/Meeting/VoteAMeeting'><input name='email' type='text' value='" + email + "' style='display:none;'></input><input name='dateIndex' type='number' value='" + chosedDate + "' style='display:none;'></input><input name='meetingID' type='number' value='" + meetingID + "' style='display:none;'></input></form> ";

    sendMailInput.textContent = ''
    tags.slice().reverse().forEach(tag => {
        let optionTag = `<option selected value = '${tag}'> ${tag} </option>`
        sendMailInput.insertAdjacentHTML("afterbegin", optionTag)
    });
    var pd1 = new Date(document.querySelector('#PlanningDate').value)
    var pd2 = new Date(document.querySelector('#PlanningDate2').value)
    var pd3 = new Date(document.querySelector('#PlanningDate3').value)
    var today = new Date();
    console.log(pd1, today);
    if (pd1 < today) {
        swal("Lütfen 1. tarih alanına geçerli bir tarih ve saat giriniz.", {
            icon: "error",
            buttons: false
        });
    } else if (pd2.getFullYear() != 1 && pd2 < today) {
        swal("Lütfen 2. tarih alanına geçerli bir tarih ve saat giriniz.", {
            icon: "error",
            buttons: false
        });
    } else if (pd3.getFullYear() != 1 && pd3 < today) {
        swal("Lütfen 3. tarih alanına geçerli bir tarih ve saat giriniz.", {
            icon: "error",
            buttons: false
        });
    } else {
        swal({
            title: "Güncelleme İşlemi",
            content: wrapper,
            icon: "warning",
            buttons: true, buttons: ["İptal Et", "Onayla"],
            dangerMode: false,
        })
            .then((willVote) => {
                if (willVote) {
                    var valdata = $("#updateForm").serialize();
                    $.ajax({
                        url: "/Meeting/EditMeeting",
                        type: "POST",
                        dataType: 'json',
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        data: valdata,
                        success: function () {
                            swal("İşlem başarılı toplantı bilgileri güncellenmiştir.", {
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
                    swal("Güncelleme işlemi iptal edildi!", {
                        icon: "info"
                    });
                }
            });
    }
}

$("#addMeetingForm").on('submit', function (e) {
    e.preventDefault();
    sendMailInput.textContent = ''
    tags.slice().reverse().forEach(tag => {
        let optionTag = `<option selected value = '${tag}'> ${tag} </option>`
        sendMailInput.insertAdjacentHTML("afterbegin", optionTag)
    });
    var valdata = $(this).serialize();
    console.log(valdata);
    const wrapper = document.createElement('div');
    wrapper.innerHTML = "Toplantı oluşturmak istediğinize emin misiniz ?<br/><br/>";
    //"<form id='voteForm' method='post' action='/Meeting/VoteAMeeting'><input name='email' type='text' value='" + email + "' style='display:none;'></input><input name='dateIndex' type='number' value='" + chosedDate + "' style='display:none;'></input><input name='meetingID' type='number' value='" + meetingID + "' style='display:none;'></input></form> ";

    var pd1 = new Date(document.querySelector('#PlanningDate').value)
    var pd2 = new Date(document.querySelector('#PlanningDate2').value)
    var pd3 = new Date(document.querySelector('#PlanningDate3').value)
    var today = new Date();
    console.log(pd1, today);
    if (pd1 < today) {
        swal("Lütfen 1. tarih alanına geçerli bir tarih ve saat giriniz.", {
            icon: "error",
            buttons: false
        });
    } else if (pd2.getFullYear() != 1 && pd2 < today) {
        swal("Lütfen 2. tarih alanına geçerli bir tarih ve saat giriniz.", {
            icon: "error",
            buttons: false
        });
    } else if (pd3.getFullYear() != 1 && pd3 < today) {
        swal("Lütfen 3. tarih alanına geçerli bir tarih ve saat giriniz.", {
            icon: "error",
            buttons: false
        });
    } else {
        swal({
            title: "Toplantı Oluşturma",
            content: wrapper,
            icon: "warning",
            buttons: true, buttons: ["İptal Et", "Onayla"],
            dangerMode: false,
        })
            .then((willVote) => {
                if (willVote) {
                    $.ajax({
                        url: "/Meeting/AddMeeting",
                        type: "POST",
                        dataType: 'json',
                        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
                        data: valdata,
                        success: function () {
                            swal("İşlem başarılı toplantı oluşturulmuştur.", {
                                icon: "success",
                                buttons: false
                            });
                            setTimeout(() => { window.location.href = "/Meeting/List"; }, 3000);
                        },
                        error: function () {
                            swal("İşleminiz gerçekleşmemiştir lütfen sonra tekrar deneyiniz.", {
                                icon: "error",
                                buttons: false
                            });
                        }
                    });


                    return false;
                } else {
                    swal("Toplantı oluşturma iptal edildi!", {
                        icon: "info"
                    });
                }
            });
    }

})


function RegisterAjax() {
    var valdata = $("#registerForm").serialize();
    $.ajax({
        url: "/Register/RegisterAjax",
        type: "POST",
        dataType: 'json',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        data: valdata,
        success: function () {
            swal("Başarılı bir şekilde kayıt oldunuz. 3 saniye sonra giriş sayfasına yönlendirileceksiniz.", {
                icon: "success",
                buttons: false
            });
            setTimeout(() => { window.location.href = "/Login/Index"; }, 3000);
        },
        error: function () {
            swal("İşleminiz gerçekleşmemiştir lütfen sonra tekrar deneyiniz.", {
                icon: "error",
                buttons: false
            });
        }
    });
}