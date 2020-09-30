using System;
using System.Diagnostics;
using WPCEmu.Rom;
using WPCEmu.Boards;
using WPCEmu.Boards.Elements;

namespace WPCEmu
{
    public class Emulator
    {
        const int TICKS_PER_MILLISECOND = 2000;
        long startTime;

        WpcCpuBoard cpuBoard;
        UiState uiFacade;

        public Emulator(RomParser.RomObject romObject)
        {
            cpuBoard = WpcCpuBoard.getInstance(romObject);
            uiFacade = UiState.getInstance(null);
        }

        void start()
        {
            Debug.Print("Start WPC Emulator");
            startTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            cpuBoard.start();
        }

        WpcCpuBoard.State getUiState(bool includeExpensiveData = true)
        {
            var uiState = cpuBoard.getState();
            var asicChangedState = uiFacade.getChangedAsicState((WpcCpuBoard.Asic)uiState.asic, includeExpensiveData);
            uiState.asic = asicChangedState;

            var runtime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds() - startTime;
            // TODO should be renamed to averageTicksPerMs
            uiState.opsMs = (int)(uiState.cpuState.tickCount / runtime);
            uiState.runtime = runtime;
            return uiState;
        }

        WpcCpuBoard.State getState()
        {
            return cpuBoard.getState();
        }

        bool? setState(WpcCpuBoard.State stateObject)
        {
            return cpuBoard.setState(stateObject);
        }

        void registerAudioConsumer(Action<SoundSerialInterface.SoundBoardCallbackData>  playbackIdCallback)
        {
            cpuBoard.registerSoundBoardCallback(playbackIdCallback);
        }

        // MAIN LOOP
        int executeCycle(int ticksToRun = 500, int tickSteps = 4)
        {
            return cpuBoard.executeCycle(ticksToRun, tickSteps);
        }

        int executeCycleForTime(int advanceByMs, int tickSteps)
        {
            int ticksToAdvance = TICKS_PER_MILLISECOND * advanceByMs;
            return executeCycle(ticksToAdvance, tickSteps);
        }

        void setCabinetInput(byte value)
        {
            cpuBoard.setCabinetInput(value);
        }

        void setSwitchInput(byte switchNr, bool? optionalValue = null)
        {
            cpuBoard.setSwitchInput(switchNr, optionalValue);
        }

        void setFliptronicsInput(string value, bool? optionalValue = null)
        {
            cpuBoard.setFliptronicsInput(value, optionalValue);
        }

        void toggleMidnightMadnessMode()
        {
            cpuBoard.toggleMidnightMadnessMode();
        }

        void setDipSwitchByte(byte dipSwitch)
        {
            cpuBoard.setDipSwitchByte(dipSwitch);
        }

        byte getDipSwitchByte()
        {
            return cpuBoard.getDipSwitchByte();
        }

        void reset()
        {
            Debug.Print("RESET!");
            startTime = new DateTimeOffset(DateTime.Now).ToUnixTimeMilliseconds();
            cpuBoard.reset();
        }

        string version()
        {
            return "0.0";
        }
    }
}

/**
* Initialize the WPC-EMU
* @function
* @param {Object} romObject, rom data (sound and main game). NOTE: DCS sound roms are not implemented yet
* @param {Object} metaData, meta data about the current game
* @return {promise} promise contains a new Emulator instance.
* @example
*
* const romObject = {
*   u06: Uint8Array(524288),
* };
* const metaData = {
*   features: ['securityPic'], // needed for WPC-S games
*   fileName: 'harr_lx2.rom',
*   skipWpcRomCheck: true,     // speedup bootup for WPC games
*   memoryPosition: [          // information about the game ram state (optional)
*    { offset: 0x3B2, description: 'current player', type: 'uint8' }
*   ]
* };
* wpcEmu.initVMwithRom(romObject, metaData)
*   .then((emu) => {
*     ...
*   }
*/