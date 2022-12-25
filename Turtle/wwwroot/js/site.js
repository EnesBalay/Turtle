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