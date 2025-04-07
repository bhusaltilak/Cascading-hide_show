$(document).ready(function () {
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
});
