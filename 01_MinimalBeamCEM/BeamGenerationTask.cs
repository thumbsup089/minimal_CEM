using PicoGK;
using System.Numerics;


public class BeamGenerationTask
{
    // Inputs 
    public double ForceN = 100;//Set Force applyed at the tip of the beam in Newton
    public double LengthMm = 100; // at what length of the beam will the force be applied
    public double SafetyFactor = 1.5;

    // fixed values for a minimal CEM
    private const double widthMm = 10.0;
    private const string Material = "EOS M 290 Aluminium AlSi10Mg"; //Material and clean scope for EOS M 290 + AlSi10Mg 
    private const double YieldStrengthMPa = 230.0;  //YieldStrengthMPa of AlSi10Mg MPa 
    private const double YoungsModulusMPa = 70000.0; // Young's modulus of AlSi10Mg ,70 GPa = 70,000 MPa = 70,000 N/mm²
    private const double MaxDeflectionMm = 0.1; // maximal biegung von 

    public void Run()
    {
        Console.WriteLine($"Length: {LengthMm} mm");
        Console.WriteLine($"Force: {ForceN} N");
        Console.WriteLine($"Safety factor: {SafetyFactor}");


        //Physikalische Regeln Hier Mechanik / Balkenbiegungstheorie

        // Maximale Biegemoment N * mm
        double maxBendingMoment =
            ForceN * LengthMm;

        // Allowed stress that can push on the bar end MPa = N/mm²
        double allowableStress =
            YieldStrengthMPa / SafetyFactor;

        //  Required height based on STRENGTH
        double heightFromStrength =
            Math.Sqrt(
                (6.0 * maxBendingMoment) /
                (widthMm * allowableStress)
            );


        // Required height based on STIFFNESS
        double heightFromDeflection = Math.Cbrt(
            (4.0 * ForceN * Math.Pow(LengthMm, 3)) /
            (YoungsModulusMPa * widthMm * MaxDeflectionMm)
        );

        // heigher height 
        double requiredHeight = Math.Max(heightFromStrength, heightFromDeflection);

        // Round UP to next 0.1 mm
        requiredHeight = Math.Ceiling(requiredHeight * 10.0) / 10.0;


        Console.WriteLine($"Width: {widthMm:F2} mm");
        Console.WriteLine($"Required Height: {requiredHeight:F2} mm");

        Console.WriteLine($"Maximum Bending Moment: {maxBendingMoment:F2} Nmm");
        Console.WriteLine($"Allowable Stress: {allowableStress:F2} MPa");


        //Building the Geometry
        // Geometry dimensions calculated by the CEM
        Vector3 beamSize = new Vector3(
            (float)LengthMm,
            (float)widthMm,
            (float)requiredHeight
        );

        // Position the beam so that x = 0 is the fixed end
        Vector3 beamCenter = new Vector3(
            (float)LengthMm / 2f,
            0f,
            (float)requiredHeight / 2f
        );

        // Create rectangular beam
        Mesh beamMesh = Utils.mshCreateCube(
            beamSize,
            beamCenter
        );

        // Convert geometry to PicoGK voxels
        Voxels beamVoxels = new Voxels(beamMesh);

        Library.oViewer().SetGroupMaterial(1, "B0B0B0", 0.1f, 0.1f);

        // Show in PicoGK viewer
        Library.oViewer().Add(beamVoxels, 1);

        beamMesh.SaveToStlFile("01_MinimalBeamCEM/output/generated_beam.stl");

    }
}

