export async function drawParkour(mPx, h, w, positions) {
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

        context.strokeStyle = "grey";
        context.stroke();

        positions.forEach(function (position) {
            let imgPadding = 0; // Use let to ensure proper block scoping

            for (const exercise of position.exercises) {
                const img = new Image();
                const currentPadding = imgPadding; // Capture imgPadding for this iteration

                img.src = exercise.src;

                img.onload = function () {
                    // Use the captured currentPadding value inside onload
                    context.drawImage(img, position.x * mPx + padding + currentPadding, position.y * mPx + padding, mPx, mPx);
                };

                img.onerror = function () {
                    console.error("Image failed to load:", exercise.src);
                };

                imgPadding += mPx; // Increment imgPadding for the next image
            }
        });
    }

    drawIt();
}
