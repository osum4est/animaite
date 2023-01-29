export function animate(animation, frames, element) {
    let frame = 0;
    setInterval(() => {
        const frameData = frames[frame];
        element.style.backgroundPosition = `-${frameData.x}px -${frameData.y}px`;
        frame = (frame + 1) % frames.length;
    }, 1000 / animation.frameRate);
}