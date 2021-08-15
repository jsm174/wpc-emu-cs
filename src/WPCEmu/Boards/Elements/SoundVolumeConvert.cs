using System.Diagnostics;

namespace WPCEmu.Boards.Elements
{
    public static class SoundVolumeConvert
    {
        /*
           function to convert 16 bit sound volume to an absolute volume (0..32)
           found values when set on screen volumes and dump commands, eg: "SET_GLOBAL_VOLUME_TO 47b8"
            FreeWPC implements this:
  	        U8 code = current_volume * 8;
		        sound_write_queue_insert (0x55);
		        sound_write_queue_insert (0xAA);
		        sound_write_queue_insert (code);
            sound_write_queue_insert (~code);
            see https://github.com/bcd/freewpc/blob/83161e2f62636cd5888af747a40d7727df1f13fa/kernel/sound.c
        */

        public static byte? getRelativeVolumeDcs(byte volumeLo, byte volumeHi)
        {
            byte complementaryVolume = (byte) (~volumeHi & 0xFF);
            if (volumeLo != complementaryVolume)
            {
                Debug.Print("WARNING, INVALID VOLUME VALUE: {0} {1}", volumeLo, volumeHi);
                return null;
            }

            return (byte)(volumeLo / 8); //Number.parseInt(volumeLo / 8, 10);
        }

        public static byte? getRelativeVolumePreDcs(byte volumeLo, byte volumeHi)
        {
            byte complementaryVolume = (byte) (~volumeHi & 0xFF);
            if (volumeLo != complementaryVolume)
            {
                Debug.Print("WARNING, INVALID VOLUME VALUE: {0} {1}", volumeLo, volumeHi);
                return null;
            }

            return volumeLo; //Number.parseInt(volumeLo, 10);
        }
    }
}
