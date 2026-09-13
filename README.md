# minimal_CEM


This repository contains a minimal example of a **Computational Engineering Model (CEM)** using C# and PicoGK.

The example generates a cantilever beam from engineering requirements instead of directly defining its final geometry.

## Inputs

- Beam length
- Applied force
- Safety factor

## Fixed assumptions

- Material: AlSi10Mg
- Rectangular cross-section
- Beam width: 10 mm
- Maximum deflection: 0.1 mm

## Engineering logic

The model uses basic beam bending theory to calculate:

1. Maximum bending moment
2. Allowable stress
3. Required beam height from strength
4. Required beam height from stiffness

The larger required height is used for the final geometry.

## Output

The resulting beam geometry is generated with PicoGK and exported as an STL file.


<img width="1537" height="777" alt="image" src="https://github.com/user-attachments/assets/5e0fbf8a-c1db-4532-864b-e0627485e8b6" />
