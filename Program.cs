using PicoGK;

var beam = new BeamGenerationTask
{
    LengthMm = 100,
    ForceN = 50,
    SafetyFactor = 1.5
};

// 0.5 mm voxel size
Library.Go(0.5f, beam.Run);