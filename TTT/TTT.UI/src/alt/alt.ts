let alt: any = null;

if ('alt' in window) {
    alt = window["alt" as any];
} else {
    alt = {
        on: (event: string, callback: (...args: any[]) => void) => { },
        emit: (event: string, ...args: any[]) => { }
    };
}

export { alt };