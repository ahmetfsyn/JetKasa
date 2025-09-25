import { useCallback, useRef } from "react";

export const useSound = () => {
  const audioRef = useRef<HTMLAudioElement | null>(null);

  const play = useCallback((path: string = "/sounds/select-sound.wav") => {
    if (
      !audioRef.current ||
      audioRef.current.src !== window.location.origin + path
    ) {
      audioRef.current = new Audio(path);
    }

    if (audioRef.current) {
      audioRef.current.currentTime = 0;
      audioRef.current.play().catch((err) => {
        console.warn("Ses oynatılamadı:", err);
      });
    }
  }, []);

  return play;
};
