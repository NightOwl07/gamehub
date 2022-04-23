export default class Color {
    static Empty = new Color(0, 0, 0, 0);
    static Transparent = new Color(0, 0, 0, 0);
    static Black = new Color(0, 0, 0, 255);
    static White = new Color(255, 255, 255, 255);
    static WhiteSmoke = new Color(245, 245, 245, 255);
    R;
    G;
    B;
    A;
    constructor(r, g, b, a = 255) {
        this.R = r;
        this.G = g;
        this.B = b;
        this.A = a;
    }
}
