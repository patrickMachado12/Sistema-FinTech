
export function formatarData(input) {
    const removeValor = input.replace(/\D/g, '');
    if (removeValor.length >= 8) {
        const day = removeValor.substring(0, 2);
        const month = removeValor.substring(2, 4);
        const year = removeValor.substring(4, 8);
        return `${day}/${month}/${year}`;
    } else {
        return removeValor
            .replace(/(\d{2})(\d{0,2})/, '$1/$2')
            .replace(/(\d{2})\/(\d{2})(\d{0,4})/, '$1/$2/$3');
    }
}

export function validarData(value) {
    const dateRegex = /^(0[1-9]|[12][0-9]|3[01])\/(0[1-9]|1[0-2])\/\d{4}$/;
    return dateRegex.test(value);
}

