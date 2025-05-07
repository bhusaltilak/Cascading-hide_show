


$(document).ready(function () {
    var provinceDropdown = $('#ProvinceDropdown');
    var districtDropdown = $('#DistrictDropdown');
    var vdcDropdown = $('#VDCDropdown');
    $('#ProvinceDropdown').change(function () {
        const provinceId = $(this).val();
        $('#vdcDiv').hide();
        $('#VDCDropdown').html('');

        if (provinceId) {
            $('#districtDiv').show();
            $.getJSON('/KYC/GetDistricts', { provinceId }, function (data) {
                let options = '<option value="">-- Select District --</option>';
                $.each(data, function (i, d) {
                    options += `<option value="${d.districtId}">${d.districtName}</option>`;
                });
                $('#DistrictDropdown').html(options);
            });
        } else {
            $('#districtDiv').hide();
            $('#DistrictDropdown').html('');
        }
    });

    $('#DistrictDropdown').change(function () {
        const districtId = $(this).val();

        if (districtId) {
            $('#vdcDiv').show();
            $.getJSON('/KYC/GetVDCs', { districtId }, function (data) {
                let options = '<option value="">-- Select VDC --</option>';
                $.each(data, function (i, v) {
                    options += `<option value="${v.vdcId}">${v.vdcName}</option>`;
                });
                $('#VDCDropdown').html(options);
            });
        } else {
            $('#vdcDiv').hide();
            $('#VDCDropdown').html('');
        }
    });
    // On page load (edit mode)
    const selectedProvince = provinceDropdown.val();
    const selectedDistrict = districtDropdown.attr("data-selected");
    const selectedVDC = vdcDropdown.attr("data-selected");

    if (selectedProvince) {
        loadDistricts(selectedProvince, selectedDistrict);
        if (selectedDistrict) {
            loadVDCs(selectedDistrict, selectedVDC);
        }
    }

});


