// แสดง/ซ่อนปุ่มลบเมื่อเลือก checkbox
function toggleDeleteButton() {
    const checkboxes = document.querySelectorAll('.email-checkbox');
    const deleteButton = document.getElementById('deleteSelectedButton');
    const anyChecked = Array.from(checkboxes).some(checkbox => checkbox.checked);

    deleteButton.classList.toggle('d-none', !anyChecked);
}

// ฟังก์ชันเลือก/ยกเลิกเลือกทั้งหมด
function toggleSelectAll(selectAllCheckbox) {
    const checkboxes = document.querySelectorAll('.email-checkbox');
    checkboxes.forEach(checkbox => {
        checkbox.checked = selectAllCheckbox.checked;
    });
    toggleDeleteButton();
}

function decodeHtmlEntities(input) {
    var txt = document.createElement("textarea");
    txt.innerHTML = input;
    return txt.value;
}

function showEmailDetails(sender, subject, date, body, id) {
    // แปลง HTML Entities ให้เป็นข้อความปกติ
    sender = decodeHtmlEntities(sender);
    subject = decodeHtmlEntities(subject);
    date = decodeHtmlEntities(date);
    body = decodeHtmlEntities(body);

    // ใส่ข้อมูลอีเมลลงใน Modal
    document.getElementById('emailSender').textContent = sender;
    document.getElementById('emailSubject').textContent = subject;
    document.getElementById('emailDate').textContent = date;
    document.getElementById('emailBody').innerText = body;
    document.getElementById('emailId').value = id;

    // เปิด Modal แสดงรายละเอียดอีเมล
    var emailModal = new bootstrap.Modal(document.getElementById('viewEmailModal'), {});
    emailModal.show();
}





