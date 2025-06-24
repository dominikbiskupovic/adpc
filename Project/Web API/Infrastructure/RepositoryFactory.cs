namespace WebApi.Infrastructure
{
    using Data;
    using Repositories;
    
    public class RepositoryFactory(AppDbContext context)
    {
        private readonly Lazy<IPatientRepository>       _patientRepo        = new(() => new PatientRepository(context));
        private readonly Lazy<IMedicalRecordRepository> _medicalRecordRepo  = new(() => new MedicalRecordRepository(context));
        private readonly Lazy<IDiseaseRepository>       _diseaseRepo        = new(() => new DiseaseRepository(context));
        private readonly Lazy<ICheckupRepository>       _checkupRepo        = new(() => new CheckupRepository(context));
        private readonly Lazy<ICheckupImageRepository>  _checkupImageRepo   = new(() => new CheckupImageRepository(context));
        private readonly Lazy<IPrescriptionRepository>  _prescriptionRepo   = new(() => new PrescriptionRepository(context));
        private readonly Lazy<IDoctorRepository>        _doctorRepo         = new(() => new DoctorRepository(context));
        private readonly Lazy<IDoctorPatientRepository> _doctorPatientRepo  = new(() => new DoctorPatientRepository(context));
        private readonly Lazy<IUserRepository>          _userRepo           = new(() => new UserRepository(context));

        public IPatientRepository       Patients        => _patientRepo.Value;
        public IMedicalRecordRepository MedicalRecords  => _medicalRecordRepo.Value;
        public IDiseaseRepository       Diseases        => _diseaseRepo.Value;
        public ICheckupRepository       Checkups        => _checkupRepo.Value;
        public ICheckupImageRepository  CheckupImages   => _checkupImageRepo.Value;
        public IPrescriptionRepository  Prescriptions   => _prescriptionRepo.Value;
        public IDoctorRepository        Doctors         => _doctorRepo.Value;
        public IDoctorPatientRepository DoctorPatients  => _doctorPatientRepo.Value;
        public IUserRepository          Users           => _userRepo.Value;
    }
}