double scale_factor = 3.5;
for (int index = 0; index < elements.Length; index++)
{
    elements[idx] = elements[idx] * scale_factor;
}

double scale_factor = 3.5;
Parallel.For(0, elements.Length-1,  // range is inclusive
    (index) =>
    {
        elements[index] = elements[index] * scale_factor;
    }
);

double sum = 0.0;
double scale_factor = 3.5;
for (int index = 0; index < elements.Length; index++)
{
    for (int innerIndex = 0; innerIndex < index; innerIndex++)
        sum  = sum + element[innerIndex] * scale_factor;
}

double sum = 0.0;
double scale_factor = 3.5;
object mutex = new object();
Parallel.For(0, elements.Length - 1,    // range is inclusive
    (index) =>
    {
        double local_sum = 0.0;
        for (int innerIndex = index; innerIndex < index; innerIndex++)
            local_sum  = local_sum + elements[innerIndex] * scale_factor;
        
        lock (mutex)
        {
            sum += local_sum;
        }
    }
);


