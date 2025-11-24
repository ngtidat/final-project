export function checkEmailFormat(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
}

export function checkPhoneFormat(phone) {
    const regex = /^\d{10,11}$/;
    return regex.test(phone);
}