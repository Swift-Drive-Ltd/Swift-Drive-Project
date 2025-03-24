## Day2
# Summary of Implemented Features in Instructor.cs

## Namespace
**DrivingLessonsBooking**

## Class
**Instructor**

### Implements
`IComparable<Instructor>`

## Properties
- **InstructorID**: `string` - Unique identifier for the instructor
- **Name**: `string` - Name of the instructor
- **Phone**: `string` - Phone number of the instructor

## Constructors
- **Default constructor**: Initializes a new instance of the Instructor class with default values
- **Parameterized constructor**: Initializes a new instance of the Instructor class with specified InstructorID, Name, and Phone

## Methods
- **CompareTo(Instructor? other)**: Compares instructors by their names
- **ToString()**: Returns a string representation of the instructor in the format `InstructorID: {InstructorID}, Name: {Name}, Phone: {Phone}`