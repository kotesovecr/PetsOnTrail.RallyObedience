export async function drawParkour(h, w) {
    var mPx = 50; // 10px = 1m
    var padding = 50;

    var height = h * mPx;
    var width = w * mPx;

    const canvas = document.querySelector("canvas");
    canvas.width = width + padding;
    canvas.height = height + padding;
    var context = canvas.getContext("2d");

    function drawIt() {
        for (var x = 0; x <= width; x += mPx) {
            context.moveTo(x + padding, padding);
            context.lineTo(x + padding, height + padding);
        }

        for (var x = 0; x <= height; x += mPx) {
            context.moveTo(padding, x + padding);
            context.lineTo(width + padding, x + padding);
        }

        context.strokeStyle = "black";
        context.stroke();
    }

    drawIt();
}
