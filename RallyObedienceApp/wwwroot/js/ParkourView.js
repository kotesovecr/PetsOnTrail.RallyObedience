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

        let exerciseCounter = 1;
        for (const position of positions) {
            let imgPadding = 0; // Use let to ensure proper block scoping

            for (const exercise of position.exercises) {
                let currentPadding = imgPadding; // Capture imgPadding for this iteration

                if (exercise.partial == false) {
                    imgPadding += mPx;

                    const squareX = position.x * mPx + padding + currentPadding;
                    const squareY = position.y * mPx + padding;

                    context.fillStyle = 'lightblue';
                    context.fillRect(squareX, squareY, mPx, mPx);

                    context.fillStyle = 'black';
                    context.font = '30px Arial';
                    context.textAlign = 'center';
                    context.textBaseline = 'middle';

                    const textX = squareX + mPx / 2;
                    const textY = squareY + mPx / 2;
                    context.fillText(exerciseCounter, textX, textY);

                    currentPadding += mPx;

                    exerciseCounter++;
                }


                // Draw image of exercise
                const img = new Image();

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
        }
    }

    drawIt();
}
